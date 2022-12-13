/**************************************************************************
Abstract: Device drivers for UCS - Unified Clock System for MSP430F5529 
Platform: TI MSP430F5529 LaunchPad
Purpose.: The purpose this module is control over UCS - setting operating
          speed to desired clock rates.
***************************************************************************
Author..: Henrik Thomsen <heth@mercantec.dk>
Company.: Mercantec ( http://www.mercantec.dk )
date....: 2018 apr.  
***************************************************************************
ToDo....: Description of problems and future extensions (TODO's)
***************************************************************************
Modification log: 
***************************************************************************
License:  Free open software but WITHOUT ANY WARRANTY.
Terms..:  see http://www.gnu.org/licenses
***************************************************************************/
#include <msp430.h>
int ucs_mode1( void );
void SetVcoreUp (unsigned int level);
//ucs_init - Initialize unified clock system
// MCLK - Master Clock - used by the CPU and system
// SMCLK - Subsystem Master Clock - Selectable by peripheral modules
// ACLK - Auxiliary Clock - Selectable by peripheral modules
//Mode 1:
//   MCLK  = 25 MHz
//   ACLK  = 32K768
//   SMCLK =  1 MHz
int ucs_init(int mode) {
  int result;
  switch ( mode ) {
    case 1: result = ucs_mode1();
       break;
  default: result = 1; // Mode not existing error
  }
  return( result );
}

int ucs_mode1( void ) {
  volatile unsigned int i;
  // Increase Vcore setting to level3 to support fsystem=25MHz
  // NOTE: Change core voltage one level at a time..
  SetVcoreUp (0x01);
  SetVcoreUp (0x02);  
  SetVcoreUp (0x03);  
  //HeTh
  // XT2 on with 4 MHz X-tal.
  // Set SMCLK = 1 MHz
  P5SEL = 0x3 << 2;      // P5.[2-3] set to XT2
  UCSCTL6 &= ~(0x3 << 14); //XT2DRIVE = 0: 4-8 MHz osc
  UCSCTL6 &= ~(0x1 << 8);  // XT2 on
  UCSCTL5 |= 0x2 << 4;  // SMCLK prescaler / 4
  __delay_cycles(1000); // XT2 settle time
  UCSCTL4 |= 0x5 << 4;  // SMCLK = XT2
  // XT1 on - low freq. mode 32K768
  P5SEL |= 0x3 << 4;    // P5.[4-5] set to XT1
  UCSCTL6 &= ~0x1;      // XT1 on
  __delay_cycles(1000000);
  UCSCTL4 &= ~( 8 << 0x7);       // ACLK = XT1CLK
 
  
  UCSCTL3 = SELREF_2;                       // Set DCO FLL reference = REFO
  //UCSCTL4 |= SELA_2;                        // Set ACLK = REFO

  __bis_SR_register(SCG0);                  // Disable the FLL control loop
  UCSCTL0 = 0x0000;                         // Set lowest possible DCOx, MODx
  UCSCTL1 = DCORSEL_7;                      // Select DCO range 50MHz operation
  //UCSCTL2 = FLLD_0 + 762;                   // Set DCO Multiplier for 25MHz
  UCSCTL2 = FLLD_1 + 762;                   // Set DCO Multiplier for 25MHz
                                            // (N + 1) * FLLRef = Fdco
                                            // (762 + 1) * 32768 = 25MHz
                                            // Set FLL Div = fDCOCLK/2
  __bic_SR_register(SCG0);                  // Enable the FLL control loop

  // Worst-case settling time for the DCO when the DCO range bits have been
  // changed is n x 32 x 32 x f_MCLK / f_FLL_reference. See UCS chapter in 5xx
  // UG for optimization.
  // 32 x 32 x 25 MHz / 32,768 Hz ~ 780k MCLK cycles for DCO to settle
  __delay_cycles(782000);

  // Loop until XT1,XT2 & DCO stabilizes - In this case only DCO has to stabilize
  do
  {
    UCSCTL7 &= ~(XT2OFFG + XT1LFOFFG + DCOFFG);
                                            // Clear XT2,XT1,DCO fault flags
    SFRIFG1 &= ~OFIFG;                      // Clear fault flags
  }while (SFRIFG1&OFIFG);    
  return(0);
}

void SetVcoreUp (unsigned int level)
{
  // Open PMM registers for write
  PMMCTL0_H = PMMPW_H;              
  // Set SVS/SVM high side new level
  SVSMHCTL = SVSHE + SVSHRVL0 * level + SVMHE + SVSMHRRL0 * level;
  // Set SVM low side to new level
  SVSMLCTL = SVSLE + SVMLE + SVSMLRRL0 * level;
  // Wait till SVM is settled
  while ((PMMIFG & SVSMLDLYIFG) == 0);
  // Clear already set flags
  PMMIFG &= ~(SVMLVLRIFG + SVMLIFG);
  // Set VCore to new level
  PMMCTL0_L = PMMCOREV0 * level;
  // Wait till new level reached
  if ((PMMIFG & SVMLIFG))
    while ((PMMIFG & SVMLVLRIFG) == 0);
  // Set SVS/SVM low side to new level
  SVSMLCTL = SVSLE + SVSLRVL0 * level + SVMLE + SVSMLRRL0 * level;
  // Lock PMM registers for write access
  PMMCTL0_H = 0x00;
}
