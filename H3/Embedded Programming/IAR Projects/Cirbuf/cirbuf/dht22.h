#ifndef TA2DHT22
#define TA2DHT22

typedef struct {
  unsigned int humidity;
  signed int temperature;
} dht22_t;

extern void dht22_init(void);
extern void dht22_read(unsigned int *dht22_data, char *event);
extern signed int dht22_convert(unsigned int *dht22arr, dht22_t *dht22);

#endif