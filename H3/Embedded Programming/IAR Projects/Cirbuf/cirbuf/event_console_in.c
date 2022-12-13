#include <stdio.h>
#include "UCA1_uart_intr.h"
#include "event_console_in.h"
#include "ht_cirbuf.h"

static char buf[CONSOLE_IN_MES_SIZE];
static signed int buf_count;
static char text[] = "Enter message: ";

void event_console_in_init(char line) {
  buf[0] = 0x00;
  buf_count = 0;
  printf("%s", CURSOR_OFF);  // Invisible Cursor
  console_gotoxy(line,1);
  printf("%s%s%c%s", text, BLINK,'_',OFF); // Fake binking '-' cursor
}
void event_console_in(char pending_events, cirbuf_str_t *cb, char *message, char line) {
       
  ht_cirbuf_pop(cb, &buf[buf_count]);
  if ( buf[buf_count] != '\r' && 
       buf[buf_count] != '\n' &&
       buf_count < CONSOLE_IN_MES_SIZE-1 ) {
    console_gotoxy(line, sizeof(text) + buf_count);
    printf("%s%c%s%c%s",OFF,buf[buf_count],BLINK,'_',OFF); // Fake binking '-' cursor
    buf_count++;                       
  } else {
    buf[buf_count] = 0x00;  // Terminate string
    message[0] = 0x00;  // Empty current message
    do {
      message[buf_count] = buf[buf_count];
      buf_count--;
    } while (buf_count >= 0);
    buf_count = 0; // Restart input
    console_gotoxy(line,1);
    printf("%s", CLR_LINE);  // Clear line
    printf("%s%s%c%s", text, BLINK,'_',OFF);
  }
}
          
    