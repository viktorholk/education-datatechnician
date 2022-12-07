#include <stdio.h>
#include <time.h>

const char format[] = "%c\nYear: %Y\nMonth: %B\nDay: %A\nWeek Number: %U\nTime: %H:%M:%S\n";

void usingTimeLibrary(){
  time_t now;
  struct tm ts;
  char buf[120];

  // Get current time
  time(&now);

  // Format time
  ts = *localtime(&now);

  strftime(buf, sizeof(buf), format, &ts);
  printf("%s\n", buf);

}


unsigned const int hour = 3600;
unsigned const int day = 86400;
unsigned const int week = 604800;
unsigned const int month = 2629743;
unsigned const int year = 31556926;

int main(void){
  usingTimeLibrary(); 


  unsigned long time_n = time(NULL);

  printf("%lu\n", time_n);

  printf("%d\n", 1970 + (time_n / year));

  printf("%d\n", (time_n / month));



  return 0;
}
