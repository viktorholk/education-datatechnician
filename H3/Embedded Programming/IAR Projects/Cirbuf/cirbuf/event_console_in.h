#define CONSOLE_IN_MES_SIZE 80
extern void event_console_in_init(char line);
extern void event_console_in(char pending_events, cirbuf_str_t *cb, char *message, char line);