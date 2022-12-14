#include <stdio.h>
#include <time.h>
#include <unistd.h>
int main(void){

  double time_spent = 0.0;

  unsigned long iterations = 100000;

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
  printf("It took %f seconds to run %lu iterations\n", time_spent, iterations);

  return 0;
}
