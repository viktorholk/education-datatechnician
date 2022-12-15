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

  for (int i = offset; i < 42; i++) {
    const int difference = dht22arr[i + 1] - dht22arr[i];
    int bit = (difference < 110) ? 0 : 1;

    bit_arr[i - offset] = bit;

    printf("%-2d %d-%d = %-3d = %d\n\r", i - offset + 1, dht22arr[i + 1],
           dht22arr[i], difference, bit);
  }
  printf("\n\r");

  for (int i = 0; i < 40; i++) {
    printf("%d", bit_arr[i]);
    if ((i + 1) % 8 == 0)
      printf(" ");
  }

  printf("\n\r");
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
  printf("\n\r");

  if (!(bytes[0] + bytes[1] + bytes[2] + bytes[3] == bytes[4]))
    return -2;

  printf("\n\r");

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
      1001, 1026, 1186, 1266, 1346, 1426, 1507, 1587, 1667, 1794, 1874,
      1967, 2047, 2127, 2207, 2335, 2463, 2590, 2717, 2811, 2891, 2971,
      3051, 3131, 3211, 3291, 3370, 3512, 3639, 3719, 3799, 3927, 4007,
      4087, 4214, 4352, 4480, 4560, 4688, 4815, 4895, 5023, 5102};

  // unsigned char bitArr[] = {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0,
  //                           1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0,
  //                           1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0};

  dht22_t data;

  int status = dht22_convert(reading, &data);

  switch (status) {
  case 0:
    printf("Humidity: %d\n\r", data.humidity);
    printf("Temperature: %d\n\r", data.temperature);
    break;
  case -2:
    printf("Parity checksum mismatch!");

    break;
  }

  return 0;
}
