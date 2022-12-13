//#include <msp430.h>
#include "io430.h"
static char volatile *ta_event;

void ta0_init(int time, char *event) {
  ta_event = event;
  TA0CTL = 0x03 << 6;   // ID:Divide clock by 8
  TA0EX0 = 0x07;        // TAIDX: Divide clock again by 8
  TA0CCTL0 = CCIE;                          // CCR0 interrupt enabled
  TA0CCR0 = time;
  // SMCLK, upmode, clear TAR and divide by 8
  TA0CTL = TASSEL_2 + MC_1 + TACLR + (0x03 << 6);
}

#pragma vector=TIMER0_A0_VECTOR
__interrupt void TIMER0_A0_ISR(void) {
  *ta_event = *ta_event + 1;
  __bic_SR_register_on_exit(CPUOFF);
}