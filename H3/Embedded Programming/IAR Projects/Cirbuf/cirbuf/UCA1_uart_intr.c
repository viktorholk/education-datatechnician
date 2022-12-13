#include <msp430.h>
//#include "io430.h"
#include <stdio.h>
#include <stdarg.h>
#include <yfuns.h> // For __read() and __write()
#include "UCA1_uart_intr.h"
#include "ht_cirbuf.h"

// USART 
// Using USCI_A1 as UART 
// Setting: 9600 baud, 8 bit, no parity


static cirbuf_str_t *cb_out;
static cirbuf_str_t *cb_in;
static long dropcount;
char volatile *in_event;
char bufout[256];
void uart_init(cirbuf_str_t *cirbuf_out, cirbuf_str_t *cirbuf_in, char *event ) {
  
  cb_out = cirbuf_out;
  cb_in = cirbuf_in;
  in_event = event;
  dropcount = 0;

  P4SEL = BIT4+BIT5;                        // P3.4,5 = USCI_A0 TXD/RXD
  UCA1CTL1 |= UCSWRST;                      // **Put state machine in reset**
  
  // 9600 Baud with ACLK = 32K768
  //UCA1CTL1 |= UCSSEL_1;                     // CLK = ACLK 32K768
  //UCA1BR0 = 0x03;                           // 32kHz/9600=3.41 (see User's Guide)
  
  // Using SMCLK = 1 MHz
  UCA1CTL1 |= UCSSEL_2;
  //UCA1BR0 = 104;           //  9600 Baud - 1 MHz / 9600 = 104
  UCA1BR0 = 52;              // 19200 Baud - 1 MHz / 19200 = 52
  
  UCA1BR1 = 0x00;                           //
  UCA1MCTL = UCBRS_3+UCBRF_0;               // Modulation UCBRSx=3, UCBRFx=0
  UCA1CTL1 &= ~UCSWRST;                     // **Initialize USCI state machine**
  // Enable recoive interrupt
  UCA1IE |= UCRXIE; //  RX interrupt enable;   
}

//cin - Character in
// Poll USCI_A0 receive register until character received.
// Parameters: none
// Returns: unsigned char - character received
unsigned char cin( void ) {
  char c;
  while (ht_cirbuf_pop(cb_in, &c) == -1)   
    __bis_SR_register(CPUOFF); // Wait for it
  return(c);
}

//cout - Character out
// Transmit character
// Parameters: Character to transmit
// Returns: none
int cout( int c ) {
  if (ht_cirbuf_push(cb_out, c) == -1 ) {   // If buffer full
    dropcount++;
    return(c); // Character silently dropped
  }
  if ( !(UCA1IE & UCTXIE) ) { // Enable interupt if not enabled - cirbuf empty
    UCA1IE |= UCTXIE;
    UCA1IFG |= 0x02;
  }
  return(c);
    
}
 
/*
  THE DLIB LOW-LEVEL I/O INTERFACE
        - See IAR C-compiler manual
        - See skeleton functions in IAR installation ./430/src/lib/dlib
*/

// __read() - DLIB-low level file i/o - defaults to console STDIN
size_t __read(int handle, unsigned char * buffer, size_t size) {
  int nChars = 0;

  /* This template only reads from "standard in", for all other file
   * handles it returns failure. */
  if (handle != _LLIO_STDIN) {
    return _LLIO_ERROR;
  }

  for (/* Empty */; size > 0; --size) {
    int c = cin();
        if (c < 0)
      break;

    *buffer++ = c;
    ++nChars;
  }

  return nChars;
}

// __write() - DLIB-low level file i/o - defaults to console STDOUT and STDERR
size_t __write(int handle, const unsigned char * buffer, size_t size) {

  size_t nChars = 0;
  
  if (buffer == 0) {
    /*
     * This means that we should flush internal buffers.  Since we
     * don't we just return.  (Remember, "handle" == -1 means that all
     * handles should be flushed.)
     */
    return 0;
  }

  /* This template only writes to "standard out" and "standard err",
   * for all other file handles it returns failure. */
  if (handle != _LLIO_STDOUT && handle != _LLIO_STDERR) {
    return _LLIO_ERROR;
  }
  for (/* Empty */; size != 0; --size) {
    if (cout(*buffer++) < 0) {
      return _LLIO_ERROR;
    }
    ++nChars;
  }
  return nChars;
}

// Console cursor to position x,y
void console_gotoxy(int x, int y) {
  printf("\033[%u;%uH",x,y);

}
void console_cls( void ) {
  printf("%s%s",CLS,HOME);
}
void console_reset( void ) {
  printf("\033c");
}
//Set textcolor 0 - 255
void console_normaltext() {
  printf("\033[0m");
}
//Set textcolor 0 - 255
void console_textcolor( unsigned char color) {
  printf("\033[38;5;%um", color );
}

//Set background color 0 - 255
void console_backgroundcolor( unsigned char color) {
  printf("\033[48;5;%um", color );
}

#pragma vector=USCI_A1_VECTOR
__interrupt void USCI_A1_ISR(void) {
  char c;
   switch(__even_in_range(UCA1IV,4))
  {
  case 0:break;                             // Vector 0 - no interrupt
  case 2:                                   // Vector 2 - RXIFG
   
    ht_cirbuf_push(cb_in, UCA1RXBUF);                  // TX -> RXed character
    *in_event = *in_event + 1;
    __bic_SR_register_on_exit(CPUOFF);
    break;
  case 4:                                    // Vector 4 - TXIFG
    if (ht_cirbuf_pop(cb_out, &c) != -1 ) { // If buffer not empty      
      UCA1TXBUF = c;
    } else {
      UCA1IE &= ~UCTXIE;  // Disable TX interrupt
    }
    break;
  default: break;
  }
}
