#include <stdio.h>
#include <time.h>

#define MINUTE_IN_SECONDS 60
#define HOUR_IN_SECONDS 3600
#define DAY_IN_SECONDS 86400
#define WEEK_IN_SECONDS 604800
#define MONTH_IN_SECONDS 2629743
#define YEAR_IN_SECONDS 31556926

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

int main(void){
  //usingTimeLibrary(); 

  unsigned long time_n = time(NULL);
  
  printf("%lu\n", time_n);

  // Years from 1970
  int second = time_n % 60;
  int minute = time_n / MINUTE_IN_SECONDS % 60;
  int hour = time_n / HOUR_IN_SECONDS % 24;
  int days = time_n / DAY_IN_SECONDS;
  int years = days / 365;
  int year = 1970 + years ;

  //int year = time_n / YEAR_IN_SECONDS;
  //int month = time_n / MONTH_IN_SECONDS / year;

  //// 1970 started with a Thursday so we add 4
  //int weekDay = ((time_n / DAY_IN_SECONDS) + 4) % 7;

  printf("Date: %d/%d/%d\n", year,days,hour);
  printf("%d/%d/%d\n", hour,minute,second);
  



  return 0;
}
