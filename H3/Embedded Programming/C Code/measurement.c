#include <stdint.h>
#include <stdio.h>

typedef struct {
  unsigned int humidity;
  signed int temperature;
} dht22_t;

int dht22_convert(unsigned int *dht22arr, dht22_t *dht22) {

  // Convert to bit array
  unsigned int bit_arr[40];

  // Scrap the first 3 readings
  const int offset = 2;

  for (int i = offset; i < 40 + offset + 1; i++) {
    const int difference = dht22arr[i + 1] - dht22arr[i];
    // printf("%d-%d = %d\n", dht22arr[i + 1], dht22arr[i], difference);
    int bit = (difference < 110) ? 0 : 1;

    bit_arr[i - offset] = bit;

    printf("%d", bit);
  }
  printf("\n");

  for (int i = 0; i < 40; i++) {
    printf("%d", bit_arr[i]);
    if ((i + 1) % 8 == 0)
      printf(" ");
  }

  printf("\n");
  // Define an array to hold the resulting bytes
  unsigned char bytes[5];

  // Iterate over the bytes in the array
  for (int i = 0; i < 5; i++) {
    for (int j = 0; j < 8; j++) {
      bytes[i] |= (bit_arr[8 * i + (7 - j)] << j);
    }
  }

  // Print the resulting bytes
  for (int i = 0; i < 5; i++) {
    printf("%-9d", bytes[i]);
  }
  printf("\n");

  // if (!(bytes[0] + bytes[1] + bytes[2] + bytes[3] == bytes[4]))
  //   return -2;

  printf("\n");

  // byte[0] = 0000 0010
  // byte[1] = 1001 0010
  // byte[0] << 8 = 0010 0000 0000
  // byte[0] << 8 | bytes[1] = 0010 0000 0000 | 1001 0010 = 0010 1001 0010
  dht22->humidity = ((bytes[0] << 8) | bytes[1]) / 10;

  // Check if minus flag is present
  signed int temperature;
  int negativeTemperatureFlag = 0;

  // If the 8 bit in the temperature byte is present it is a flag that the
  // temperature is negative
  if (bytes[2] & 0x80) {
    negativeTemperatureFlag = 1;

    // Clear flag
    bytes[2] &= ~0x80;
  }

  temperature = ((bytes[2] << 8) | bytes[3]) / 10;

  // Convert to negative number if the flag has been set
  if (negativeTemperatureFlag)
    temperature = ~(temperature) + 1;

  dht22->temperature = temperature;
  return 0;
}

int main(void) {

  unsigned int reading[] = {
      1001, 1026, 1186, 1266, 1346, 1426, 1506, 1586, 1667, 1747, 1873,
      2014, 2142, 2222, 2350, 2478, 2558, 2685, 2764, 2858, 2938, 3018,
      3098, 3178, 3258, 3339, 3418, 3559, 3686, 3767, 3894, 3974, 4102,
      4182, 4261, 4399, 4479, 4607, 4687, 4815, 4943, 5070, 5197};

  // unsigned char bitArr[] = {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0,
  //                           1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0,
  //                           1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0};

  dht22_t data;

  int status = dht22_convert(reading, &data);

  switch (status) {
  case 0:
    printf("Humidity: %d\n", data.humidity);
    printf("Temperature: %d\n", data.temperature);
    break;
  case -2:
    printf("Parity checksum mismatch!");

    break;
  }

  return 0;
}
