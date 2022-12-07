
#include "io430.h"
#include <stdio.h>

int main(void){
  // Stop watchdog timer to prevent time out reset
  WDTCTL = WDTPW + WDTHOLD;
  
  uart_init();
    printf("%u", (unsigned)time(NULL));


  while(1);
}