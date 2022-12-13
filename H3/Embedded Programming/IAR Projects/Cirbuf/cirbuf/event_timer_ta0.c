#include <stdio.h>
#include "UCA1_uart_intr.h"

static long count=0;

void event_timer_ta0(char pending_events, char *message, char line) {
  count++;
  console_gotoxy(line,1);
  printf("Pending events: ");
  printf("%d , count: %ld  - Message: %s%s", pending_events, count, message, CLR_EOL);
}