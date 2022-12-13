#include "dht22.h"

void event_dht22_in(char *event, dht22_t *dht22){
  unsigned int dht22_data[43];
  dht22_read(dht22_data, event);
  
  int status = dht22_convert(dht22_data, dht22);
  
}