
#include "io430.h"

int main( void )
{
  // Stop watchdog timer to prevent time out reset
  WDTCTL = WDTPW + WDTHOLD;
  
  P4DIR = 0xff;
  
  P4OUT = 0xff;
  
  __delay_cycles(50000);
  
  P4OUT &= 0x00;
  
  while(1);
}
