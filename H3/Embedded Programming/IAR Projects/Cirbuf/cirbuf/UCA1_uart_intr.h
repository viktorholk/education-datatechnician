#include "ht_cirbuf.h"
extern void uart_init(cirbuf_str_t *cirbuf_out, cirbuf_str_t *cirbuf_in, char *event);
extern unsigned char cin( void );
extern int cout( int c );

extern void console_gotoxy(int x, int y);

extern void console_cls( void );
extern void console_reset( void );
extern void console_normaltext();
extern void console_textcolor( unsigned char color);
extern void console_backgroundcolor( unsigned char color);


//Some vt100 Escape sequences
#define OFF             "\033[0m"
#define BOLD            "\033[1m"
#define LOWINTENS       "\033[2m"
#define ITALIC          "\033[3m"
#define UNDERLINE       "\033[4m"
#define BLINK           "\033[5m"
#define REVERSE         "\033[7m"

// POSITIONING
#define CLS             "\033[2J"       // Esc[2J Clear entire screen
#define CLR_LINE        "\033[2K"       // Clear line cursor is ón
#define CLR_EOL         "\033[0K"       // Clear end_of_line - to the right of cursor
#define CLR_SOL         "\033[1K"       // Clear start_of_line - to the left of the cursor
//#define GOTOXY(x,y)    "\033[x;yH"     // Esc[Line;ColumnH
#define GOTOXY(x,y)    "[x;y]"     // Esc[Line;ColumnH
#define HOME            "\033[H"

#define CUD(x)          "\033[xB"      // Move cursor up n lines
#define CURSOR_OFF      "\033[?25l"
#define CURSOR_ON       "\033[?25h"

//ASC-II box drawing symbols - single line
#define BOXV      179      // Vertical Line |
#define BOXVL     180      // Vertical line left line -|
#define BOXUPC    191      // Upper rigth corner -.
#define BOXLLC    192      // Lower left corner |_
#define BOXHU     193      // Horizontal and up _|_
#define BOXHD     194      // Horizontal and down -.-
#define BOXVR     195      // Vertical line right line |-
#define BOXH      196      // Horizontal line -
#define BOXX      197      // Intersection -|-
