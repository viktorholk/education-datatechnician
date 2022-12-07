
#include "io430.h"
#include <stdio.h>
#include "USCI1.h"
#include <time.h>

int main( void )
{
  // Stop watchdog timer to prevent time out reset
  WDTCTL = WDTPW + WDTHOLD;
  uart_init();
  
  double time_spent = 0.0;

  unsigned long iterations = 10000;

  float result = 0;
  int divider = 1;
  
  printf("Calculating PI with %lu iterations ...\n",iterations);
  
  clock_t begin = clock();
  for (int i = 1; i <= iterations; i++){

    float value = 1.0 / divider;

    if (i % 2)
      result += value;
    else
      result -= value;
      
    divider += 2;
  }
  
  clock_t end = clock();

  time_spent += (double)(end - begin) / CLOCKS_PER_SEC;

  float pi = 4 * result;
  
  printf("Result: %f\n", pi);
  printf("It took %.3f seconds to run %lu iterations\n", time_spent, iterations);

  while(1);
}


