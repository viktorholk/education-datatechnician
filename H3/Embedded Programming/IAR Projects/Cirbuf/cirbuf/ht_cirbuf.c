#include "ht_cirbuf.h"
/*
 * Module...: ht_cirbuf.c
 * Version..: 0.1 (Beta - unfinished)
 * Author...: Henrik Thomsen/Mercantec <heth@mercantec.dk
 * Date.....: 27. maj 2022
 *
 * Abstract.: Implementation of simple circular buffer for
 *            embedded systems with limited resources.
 * Target...: MSP430
 * Docs.....:
 * Mod. log.:
 * License..: Free open software but WITHOUT ANY WARRANTY.
 * Terms....: see http://www.gnu.org/licenses
 *
 */



void ht_cirbuf_init(cirbuf_str_t *cb, char *buffer, int buffer_length) {
        cb->buffer = buffer;
        cb->length = buffer_length;
        cb->head = 0;
        cb->tail = 0;
        cb->debug_count = 0;
        cb->debug_dropped =0;
}

int ht_cirbuf_push(cirbuf_str_t *cb, char data) {
        int next;

    next = cb->head + 1;  // next is where head will point to after this write.
    if (next >= cb->length)
        next = 0;

    if (next == cb->tail) { // if the head + 1 == tail, circular buffer is full
      cb->debug_dropped++;  
      return -1;
    }

    cb->buffer[cb->head] = data;  // Load data and then move
    cb->head = next;             // head to next data offset.
    // Debug
    cb->debug_count++;
    return 0;  // return success to indicate successful push.
}

int ht_cirbuf_pop(cirbuf_str_t *cb, char *data) {
            int next;

    if (cb->head == cb->tail)  // if the head == tail, we don't have any data
        return -1;

    next = cb->tail + 1;  // next is where tail will point to after this read.
    if(next >= cb->length)
        next = 0;

    *data = cb->buffer[cb->tail];  // Read data and then move
    cb->tail = next;              // tail to next offset.
    return 0;  // return success to indicate successful pop.
}

