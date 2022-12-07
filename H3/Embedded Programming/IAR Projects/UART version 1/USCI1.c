#include "io430.h"


void uart_init( ) {

  P4SEL = BIT4+BIT5;                        // P3.4,5 = USCI_A0 TXD/RXD
  UCA1CTL1 |= UCSWRST;                      // **Put state machine in reset**
  UCA1CTL1 |= UCSSEL_1;                     // CLK = ACLK
  UCA1BR0 = 0x03;                           // 32kHz/9600=3.41 (see User's Guide)
  UCA1BR1 = 0x00;                           //
  UCA1MCTL = UCBRS_3+UCBRF_0;               // Modulation UCBRSx=3, UCBRFx=0
  UCA1CTL1 &= ~UCSWRST;                     // **Initialize USCI state machine**
   
}

//cin - Character in
// Poll USCI_A0 receive register until character received.
// Parameters: none
// Returns: unsigned char - character received
unsigned char cin( void ) {
  while ( !(0x01 & UCA1IFG) );
  return(UCA1RXBUF);
}
//cin_nonblocked
//If character received - return it
//Else return 0x00 - No character received
 unsigned char cin_nonblocked( void ) {
  if ( 0x01 & UCA1IFG ) {
    return(UCA1RXBUF);
  } else {
    return(0x00);
  }
}
//cou - Character out
// Transmit character poll USCI_A0 transmit register until ready.
// Parameters: Character to transmit
// Returns: none
void cout( unsigned char c ) {
  while ( !(0x02 & UCA1IFG) ); //&& (count++ > 0) );
  UCA1TXBUF = c;
}
int putchar( int charout ) {
  cout( (char) charout );
  return(charout);
}