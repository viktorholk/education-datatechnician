//#include <msp430.h>
#include "io430.h"
#include <stdio.h>
#include "UCA1_uart_intr.h"
#include "ta0.h"
#include "UCS.h"
#include "ht_cirbuf.h"
#include "event_timer_ta0.h"
#include "event_console_in.h"
#include "dht22.h"
#define EVENTS 3
enum  event_types {
  CONSOLE_IN    = 0x00,
  TIMER_TA0     = 0x01,
  DHT22         = 0x02
};
char event[EVENTS];

char con_out_buf[2500];
char con_in_buf[64];
cirbuf_str_t cb_out;
cirbuf_str_t cb_in;

char message[CONSOLE_IN_MES_SIZE];

unsigned int dht22_arr[43];
dht22_t data;

int main(void) {
  
  // Initialize  
  
  WDTCTL = WDTPW + WDTHOLD;
  ucs_init(1);  // Default MCLK=SMCLK=~ 1 MHz, ACLK=~32K768
  ht_cirbuf_init(&cb_out, con_out_buf, sizeof(con_out_buf));
  ht_cirbuf_init(&cb_in, con_in_buf, sizeof(con_in_buf));
  uart_init(&cb_out, &cb_in, &event[CONSOLE_IN]);  
  ta0_init(1250, &event[TIMER_TA0]); // ta0 clock: 1.000.000 / 8 = 125 Khz
  
  message[0] = 0x00; // Empty string
  //Initialize events array to 0 events
  for (int i=0; i < EVENTS; i++)
    event[i] = 0;
  
  __bis_SR_register(GIE); 
  
  //Prepare console nicely :-)
  console_reset();           // Clear and reset terminal
  console_cls();
  
  //event_console_in_init(6);
  
  printf("DHT22 Program\n\r");
  
  dht22_read(dht22_arr, &event[DHT22]);
  
  for (int i = 0; event[DHT22] == 0; i++);
  
  // Only pass the 40 readings
  
  int status = dht22_convert(dht22_arr, &data);
  
  switch (status) {
  case 0:
    printf("Humidity: %d\n\r", data.humidity);
    printf("Temperature: %d\n\r", data.temperature);
    break;
  case -2:
    printf("Parity checksum mismatch!");
    break;
  default:
    printf("Something went wrong");
    break;
  }  
  
  while(1);
  
}



