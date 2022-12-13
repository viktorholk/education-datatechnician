#ifndef HT_CIRBUF
#define HT_CIRBUF
typedef struct cirbuf_structs {
    char        *buffer;
    int         length;
    int         head;
    int         tail;
    long long   debug_count;
    long        debug_dropped;
} cirbuf_str_t;

extern void ht_cirbuf_init(cirbuf_str_t *cb, char *buffer, int buffer_length);
extern int ht_cirbuf_push(cirbuf_str_t *cb, char data);
extern int ht_cirbuf_pop(cirbuf_str_t *cb, char *data);
#endif