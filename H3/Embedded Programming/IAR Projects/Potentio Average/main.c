#include <msp430.h>
#include <stdio.h>


void ADC_init(){
  ADC12CTL0 = ADC12SHT02 + ADC12ON;    // Sampling time, ADC12 on
  ADC12CTL1 = ADC12SHP;           // Use sampling timer
  //ADC12IE = 0x01;                          // Enable interrupt
  ADC12CTL0 |= ADC12ENC;
  P6SEL |= 0x01;                               // P6.0 ADC option select
  
}

void LED_init(){
  P1DIR |= BIT0;
  P4DIR |= BIT7;
  // Clear output
  P1OUT &= ~BIT0;
  P4OUT &= ~BIT7;
}

void button_init(){
  P1REN |= BIT1;                            // Enable P1.1 internal resistance
  P1OUT |= BIT1;                            // Set P1.1 as pull-Up resistance
  P1IES &= ~BIT1;                           // P1.1 Lo/Hi edge
  P1IFG &= ~BIT1;                           // P1.1 IFG cleared
  P1IE |= BIT1;                             // P1.1 interrupt enabled
}

void timerA_init(){
  TA1CCTL0 = CCIE;
  TA1CTL = TASSEL_2 + MC_2 + TACLR + ID_3;
}

int main(void)
{
  WDTCTL = WDTPW + WDTHOLD;                 // Stop WDT
  uart_init();
  printf(".\n");
  
  ADC_init();
  
  button_init();
  LED_init();
  //timerA_init();
  
  P1OUT |= BIT0;            // P1.0 = 1
  
  __bis_SR_register(GIE);
  
  while(1);
  
}

#pragma vector=PORT1_VECTOR
__interrupt void Port_1(void)
{
  printf("xxxx pressed");
  if (P1IFG & BIT1) { // P1.3 interrupt?
    
    P1OUT &= ~BIT0;
    P4OUT |= BIT7;
    
    const short len = 200;
    long sum = 0;
    
    unsigned short buffer[200];
    
    for (int i = 0; i < len; i++){
      ADC12CTL0 |= ADC12SC;            // Start sampling/conversion
      while ( ADC12CTL1 & ADC12BUSY );  
      unsigned short reading = ADC12MEM0;
      
      const int value = reading / 40.95;
      buffer[i] = value;
      sum += value;
      
      printf("[%d]Reading: %d%\n", i,value);
      __delay_cycles(5000);
    }
    
    const int average = sum / len;
    
    printf("Average: %d%\n", average);
    
    P1OUT |= BIT0;
    P4OUT &= ~BIT7;
    
    P1IFG &= ~BIT1;   // clear the P1.3 interrupt flag
  }
}


#pragma vector=TIMER1_A0_VECTOR
__interrupt void TIMER1_A0_ISR(void)
{

}