/*
- Purpose: Measure temperature and humidity with DHT22/AM302 sensor
- Board: MSP430F5529 LaunchPad
- Peripheral: Seeed Studio: Grove - Temperature & Humidity Sensor Pro (DHT22/AM2302) - v1.3
- DHT22/AM2302 signal on pin P2.5 configured to TA2.2 - CCI2A (Grove board pin 40)
- SMCLK: 1 MHz
- Author: Henrik Thomsen <heth@mercantec.dk>
- date: 08. dec 2022
- Modificatiopn log:
*/
/* NOTES on DHT22:
- Need a minimum of 2 seconds startup time
- 5K Resitor pullup. (On seeed grove DHT22 board)
- Minmum 1 mS start signal from host (Pull down)
- Release start signal for 50 uS (High - pulled up)
- Change pin for input (start capture mode falling edge)
- Expect sensor response signal is 2 x 80 uS = 160 uS ( < 170 uS and > 150 uS )
- Read 40 databits on falling edge on Capture TA2.2
- 0 = 76 uS  ( < 85 uS and > 70uS )
- 1 = 120 uS ( < 130 uS and > 116 uS )
- Sensor release time max 55 uS
- MAX read time:
RESPONSE_TIME_MAX + 40 * DATABITS_MAX = 170 uS + 40 * 130 uS = 5370 uS

Datasheet: https://github.com/SeeedDocument/Grove-Temperature_and_Humidity_Sensor_Pro/raw/master/res/AM2302-EN.pdf

Method: 
CCR1: Compare mode: Time start signal - then used as Timeout error
CCR2: Capture mode: Falling edge time 0 or 1 from device

Interrupt driven time measurement of the response signal + 40 databits.
TA2R set to 0 and start counter when host release start signal
CCR2 trigger interrupt on falling edge storing and stores TA2R in
42 unsigned word array for later processing.

All values in array are readings from TA2 SAR counter in 1 uS count
if any errors occur the corresponding array member is et to 0xFFFF to
indicate the error and where it happened in the sequence

- array[0] = Start signal time
- array[1] = Bus master release time
- array[2] = Response signal time
- array[3-42] are data bits
- Zeros are between 70 and 85 uS (From datasheet)
- Ones are between 116 and 130 uS (From datasheet)
Experimentaly 0 and 1 timing is not all within time limits - based on one device!!!
- Need more data/evidence before used in a production!!!!!!!
- Zeros up 94 uS - smallest 78 uS
- Ones up to 142 uS - smallest 124 uS
*/
#include <io430.h>
#include "dht22.h"
#include <stdio.h>
#include <stdint.h>
//DHT22ARRAYSIZE explained above in comment
#define ARRAYSIZE 43
#define ARRAYFIRSTBIT 3
#define STARTSIGNAL 1000 // Startsignal duration = 1000 uS
#define ZERO_MIN 70        // 70 uS from datasheet
#define ZERO_MAX 100       // Experimentaly found and rounded up a litte
#define ONE_MIN 116        // 116 uS from datasheet
#define ONE_MAX 160        // Experimentaly found and rounded up a litte


//############# Module static
struct module_static {
  char *event;
  unsigned int *dht22_array;
  char sequence;
};

static struct module_static ms;   
//#############

/*
Function: dht22_read
Abstract: Initiate retrival of data from DHT22 temperature and humidity module
When data ready in dht22_timing array, event is incremented by 1
REQUIRED: SMCLK = 1 MHz

Implementation:
DHT22 module is hardwired to P2.5 which alternate function is TA2 CCI2A

1: Set TA2 to SMCLK in stopmode
2: Set TA2 CCR1 to compare mode, TA2CCR1 = 1000 ( 1 mS)
3: Set TA2 CCR2 to capture mode with CCI2A on fallling edge
4: Check P2.5 level is 1 - 5K pullup from DHT22 (100 uS use timer?????)
5: Set P2.5 as low output (never high!!)- startsignal beginning
P2.5 has no open-drain possebility - never set output high!!!
6: 6: Start timer = Continuous mode and enable interrupt on TA2CCR1      
7: Done here - ISR TA2 takes over
*/
void dht22_read(unsigned int *dht22_array, char *event) {
  ms.event = event;
  ms.dht22_array = dht22_array;
  for (int i=0; i < ARRAYSIZE; i++, dht22_array[i] = 0);
  ms.sequence = 0;  // Incremented on each interrupt and used as pointer to ms.dht22_timing array
  // 1: Set TA2 to SMCLK in stopmode
  TA2CTL = TACLR;   // Clear timer and TA2CTL settings
  TA2CTL = TASSEL1; //Continuous up count to 0xFFFF in stopmode
  TA2EX0 = 0;       // TAIDEX divide by 1
  // 2: Set TA2 CCR1 to compare mode, TA2CCR1 = 1000 ( 1 mS)
  TA2CCTL1 = 0; // Default setting
  TA2CCR1 = STARTSIGNAL; 
  // 3: Set TA2 CCR2 to capture mode with CCI2A on fallling edge
  TA2CCTL2 = 0; // Default setting
  TA2CCTL2 = CAP | SCS | CM1;
  // 4: Check P2.5 level is 1 - 5K pullup from DHT22 (100 uS use timer?????)
  P2REN &= ~(BIT5);     // P2.5 = Resistor disable
  P2DIR &= ~(BIT5);     // P2.5 = input
  if ( !(P2IN & BIT5)) {        // P2.5 must be high to indicate DHT22 might be ready!
    dht22_array[0] = 0xffff;    // Indicate error
    *event++;                   // Set event
    return;
  }
  // 5: Set P2.5 as low output (never high!!)- startsignal beginning
  P2OUT &= ~BIT5;  // Must be set to 0 before direction out
  P2DIR  |= BIT5;
  // 6: Start timer = Continuous mode and enable interrupt on TA2CCR1
  TA2CCTL1 |= CCIE;
  TA2CTL |= MC1; // Start timer in continuous up count to 0xFFFF
  // 7: Done here - ISR TA2 takes over
  
}

//Converts dht22_array with timer values to temperature and humidity
// Return 0 on succes
// Return -1 to indicate invalid timer value or other error from ISR
// return -2 to indicate parity error
int dht22_convert(unsigned int *dht22arr, dht22_t *dht22) {
  // Convert to bit array
  unsigned int bit_arr[40];

  for (int i = 0; i < 43; i++) {
    printf("%d ", dht22arr[i]);
  }
  printf("\n\r");

  // Scrap the first 3 readings
  const int offset = 2;

  for (int i = offset; i < 42; i++) {
    const int difference = dht22arr[i + 1] - dht22arr[i];

    int bit = (difference < 110) ? 0 : 1;

    bit_arr[i - offset] = bit;

    printf("%-2d %d-%d = %-3d = %d\n\r", i - offset, dht22arr[i + 1],
           dht22arr[i], difference, bit);
  }
  printf("\n\r");

  for (int i = 0; i < 40; i++) {
    printf("%d", bit_arr[i]);
    if ((i + 1) % 8 == 0)
      printf(" | ");
  }

  printf("\n\r");
  // Define an array to hold the resulting bytes
  unsigned char bytes[5];

  // Iterate over the bytes in the array
  for (int i = 0; i < 5; i++) {
    for (int j = 0x80, k = 0; j > 0; j = j >> 1, k++) {
      if (bit_arr[(i * 8) + k] == 0) {
        bytes[i] &= ~j;
      } else
        bytes[i] |= j;
    }
  }

  // Print the resulting bytes
  for (int i = 0; i < 5; i++) {
    printf("%-11d", bytes[i]);
  }
  printf("\n\r");

  if ((unsigned char)(bytes[0] + bytes[1] + bytes[2] + bytes[3]) != bytes[4])
    return -2;

  printf("\n\r");

  // byte[0] = 0000 0010
  // byte[1] = 1001 0010
  // byte[0] << 8 = 0010 0000 0000
  // byte[0] << 8 | bytes[1] = 0010 0000 0000 | 1001 0010 = 0010 1001 0010
  dht22->humidity = ((bytes[0] << 8) | bytes[1]) / 10;

  // Check if minus flag is present
  signed int temperature;
  int negativeTemperatureFlag = 0;

  // If the 8 bit in the temperature byte is present it is a flag that the
  // temperature is negative
  if (bytes[2] & 0x80) {
    negativeTemperatureFlag = 1;

    // Clear flag
    bytes[2] &= ~0x80;
  }

  temperature = ((bytes[2] << 8) | bytes[3]) / 10;

  // Convert to negative number if the flag has been set
  if (negativeTemperatureFlag)
    temperature = ~(temperature) + 1;

  dht22->temperature = temperature;

  for (int i = 0; i < 2; i++) {

    if (!i)
      printf("Calculate Humidity:    ");
    else
      printf("Calculate Temperature: ");

    for (int j = 0; j < 16; j++) {
      printf("%d", bit_arr[i * 16 + j]);
      if ((j + 1) % 8 == 0)
        printf(" ");
    }
    if (!i) {
      printf("= %xH = %d / 10 = %d", ((bytes[0] << 8) | bytes[1]),
             ((bytes[0] << 8) | bytes[1]), ((bytes[0] << 8) | bytes[1]) / 10);
    } else
      printf("= %xH = %d / 10 = %d", ((bytes[2] << 8) | bytes[3]),
             ((bytes[2] << 8) | bytes[3]), ((bytes[2] << 8) | bytes[3]) / 10);

    printf("\n\r");
  }

  printf("\n\r");

  return 0;
}

#pragma vector=TIMER2_A1_VECTOR
__interrupt void timer2_a1_isr(void) {
  if (ms.sequence >= ARRAYSIZE) { // Array overflow if here!
    ms.sequence--;
    goto error_exit;
  }
  switch (TA2IV) { //Interrupt vector from TA2
  case 0x02:       // TA2CCR1 generated Interrupt
    if (TA2CCR1 == STARTSIGNAL) { //Start signal elapsed
      ms.dht22_array[ms.sequence++] = TA2R;  // Record timer count - just fort fun
      TA2CCR1 = 0xffff; // Now use for timeout
      // Prepare P2.5 as input for TA2 CCIA2A
      P2DIR &= ~BIT5;
      P2SEL |= BIT5;
      // Start TA2CCR2 as capture on falling edge and enable interrupt
      TA2CCTL2 = CAP | SCS | CM1 | CCIE;
      return;
    }
    goto error_exit;
    break;
  case 0x04:       // TACCR2 generated Interrupt
    ms.dht22_array[ms.sequence++] = TA2CCR2;
    if ((TA2CCTL2 |= COV) == COV) // Capture overflow error
      goto error_exit;
    if  (ms.sequence == ARRAYSIZE)  // Done?
      goto ok_exit;
    return;
    break;
  case 0x0e:       // Timer overflow
    goto error_exit;
    break;
  default:      // Ignore ??
    goto error_exit;
    break;
  }
error_exit:
  ms.dht22_array[ms.sequence] = 0xffff;  // Set error in sequence
  
ok_exit:
  TA2CTL &= ~(TACLR | TAIE | TAIFG);       // Stop TA2 and disable and clear Interrupts
  TA2CCTL1 &= ~CCIE;
  TA2CCTL2 &= ~CCIE;
  
  (*ms.event)++;                          // Set event to indicate finish
  return; 
}