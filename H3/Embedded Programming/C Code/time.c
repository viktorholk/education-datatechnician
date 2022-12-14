#include <stdio.h>
#include <time.h>

#define MINUTE_IN_SECONDS 60
#define HOUR_IN_SECONDS 3600
#define DAY_IN_SECONDS 86400
#define WEEK_IN_SECONDS 604800
#define MONTH_IN_SECONDS 2629743
#define YEAR_IN_SECONDS 31556926

const char format[] =
    "%c\nYear: %Y\nMonth: %B\nDay: %A\nWeek Number: %U\nTime: %H:%M:%S\n";

void usingTimeLibrary() {
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

// Days
static const char weekDays[7][10] = {"Monday",   "Tuesday", "Wednesday",
                                     "Thursday", "Friday",  "Saturday",
                                     "Sunday"};
// Months
static const char months[12][10] = {
    "January", "February", "March",     "April",   "May",      "June",
    "July",    "August",   "September", "October", "November", "December"};

typedef struct DateTime {
  int timestamp;

  int year;
  int month;
  int day;

  int week;
  int weekDay;

  int seconds;
  int minutes;
  int hours;
} DateTime;

// DateTime ConvertUnixToDateTime(int timestamp) {
//   DateTime dateTime;
//   dateTime.timestamp = timestamp;
//
//   // Calculate the time
//   dateTime.seconds = timestamp % 60;
//   dateTime.minutes = timestamp / MINUTE_IN_SECONDS % 60;
//   dateTime.hours = timestamp / HOUR_IN_SECONDS % 24;
//
//   // Calculate the date
//   int years = timestamp / YEAR_IN_SECONDS;
//
//   int secondsInCurrentYear = timestamp % YEAR_IN_SECONDS;
//
//   dateTime.year = 1970 + years;
//   dateTime.month = secondsInCurrentYear / MONTH_IN_SECONDS;
//
//   dateTime.day = secondsInCurrentYear % MONTH_IN_SECONDS / DAY_IN_SECONDS;
//   printf("%d\n", dateTime.day);
//
//   dateTime.week = secondsInCurrentYear / DAY_IN_SECONDS / 7;
//   dateTime.weekDay = (timestamp / DAY_IN_SECONDS + 3) % 7;
//
//   return dateTime;
// }

int is_leap_year(int year) {
  if (year % 4 != 0) {
    return 0; // Not divisible by 4, not a leap year.
  }
  if (year % 100 != 0) {
    return 1; // Divisible by 4 but not by 100, a leap year.
  }
  if (year % 400 != 0) {
    return 0; // Divisible by 100 but not by 400, not a leap year.
  }
  return 1; // Divisible by 400, a leap year.
}

// Returns the number of days in the given month and year.
int days_in_month(int month, int year) {
  if (month == 4 || month == 6 || month == 9 || month == 11) {
    return 30; // April, June, September, and November have 30 days.
  }
  if (month == 2) {
    if (is_leap_year(year)) {
      return 29; // February has 29 days in a leap year.
    }
    return 28; // February has 28 days in a non-leap year.
  }
  return 31; // All other months have 31 days.
}

// Converts a Unix timestamp to a human-readable date.
DateTime ConvertUnixToDateTime(int timestamp) {
  // days since epoch
  int days = timestamp / DAY_IN_SECONDS;

  // Initialize date from epoch start
  int year = 1970;
  int month = 1;
  int day = 1;

  // Increment the year and decrement the number of days until the correct year
  // has been found.
  while (days >= 365) {
    if (is_leap_year(year)) {
      if (days >= 366) {
        // Leap year and there are enough days remaining to include an extra day
        // in February.
        days -= 366;
        year++;
      }
    } else {
      days -= 365;
      year++;
    }
  }

  // Increment the month and decrement the number of days until the correct
  // month has been found.
  while (days >= days_in_month(month, year)) {
    days -= days_in_month(month, year);
    month++;
  }

  // The remaining days is the current day
  day = days;

  // Remaining seconds in day
  DateTime dt;

  dt.timestamp = timestamp;
  dt.year = year;
  dt.month = month;
  dt.day = day;

  dt.seconds = timestamp % 60;
  dt.minutes = timestamp / MINUTE_IN_SECONDS % 60;
  dt.hours = timestamp / HOUR_IN_SECONDS % 24;

  dt.weekDay = (timestamp / DAY_IN_SECONDS + 3) % 7;
  dt.week = timestamp % YEAR_IN_SECONDS / DAY_IN_SECONDS / 7;

  return dt;
}

void PrintDateTime(DateTime *dateTime) {
  printf("Timestamp: %d\n", dateTime->timestamp);
  printf("Date: %d/%d/%d\n", dateTime->day, dateTime->month, dateTime->year);
  printf("      %s %s\n", weekDays[dateTime->weekDay],
         months[dateTime->month - 1]);
  printf("Week: %d\n", dateTime->week);
  printf("Time: %d:%d:%d\n", dateTime->hours + 1, dateTime->minutes,
         dateTime->seconds);
  printf("\n");
}

int main(void) {
  //usingTimeLibrary();

  unsigned long timestamp = time(NULL);

  DateTime currentDateTime = ConvertUnixToDateTime(timestamp);

  PrintDateTime(&currentDateTime);

  DateTime testDateTime = ConvertUnixToDateTime(1016713530);

  PrintDateTime(&testDateTime);

  return 0;
}
