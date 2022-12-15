#include <stdint.h>
#include <stdio.h>

typedef struct {
  unsigned int humidity;
  signed int temperature;
} dht22_t;

int dht22_convert(unsigned int *dht22arr, dht22_t *dht22) {
  // Convert to bit array
  unsigned int bit_arr[40];

  for (int i = 0; i < 43; i++) {
    printf("%d ", dht22arr[i]);
  }
  printf("\n\r");

  // Scrap the first 3 readings
  const int offset = 2;

  for (int i = offset; i < 42; i++) {
    const int difference = dht22arr[i + 1] - dht22arr[i];

    int bit = (difference < 110) ? 0 : 1;

    bit_arr[i - offset] = bit;

    printf("%-2d %d-%d = %-3d = %d\n\r", i - offset, dht22arr[i + 1],
           dht22arr[i], difference, bit);
  }
  printf("\n\r");

  for (int i = 0; i < 40; i++) {
    printf("%d", bit_arr[i]);
    if ((i + 1) % 8 == 0)
      printf(" | ");
  }

  printf("\n\r");
  // Define an array to hold the resulting bytes
  unsigned char bytes[5];

  // Iterate over the bytes in the array
  for (int i = 0; i < 5; i++) {
    for (int j = 0x80, k = 0; j > 0; j = j >> 1, k++) {
      if (bit_arr[(i * 8) + k] == 0) {
        bytes[i] &= ~j;
      } else
        bytes[i] |= j;
    }
  }

  // Print the resulting bytes
  for (int i = 0; i < 5; i++) {
    printf("%-11d", bytes[i]);
  }
  printf("\n\r");

  if ((unsigned char)(bytes[0] + bytes[1] + bytes[2] + bytes[3]) != bytes[4])
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

  for (int i = 0; i < 2; i++) {

    if (!i)
      printf("Calculate Humidity:    ");
    else
      printf("Calculate Temperature: ");

    for (int j = 0; j < 16; j++) {
      printf("%d", bit_arr[i * 16 + j]);
      if ((j + 1) % 8 == 0)
        printf(" ");
    }
    if (!i) {
      printf("= %xH = %d / 10 = %d", ((bytes[0] << 8) | bytes[1]),
             ((bytes[0] << 8) | bytes[1]), ((bytes[0] << 8) | bytes[1]) / 10);
    } else
      printf("= %xH = %d / 10 = %d", ((bytes[2] << 8) | bytes[3]),
             ((bytes[2] << 8) | bytes[3]), ((bytes[2] << 8) | bytes[3]) / 10);

    printf("\n\r");
  }

  printf("\n\r");

  return 0;
}

int main(void) {

  unsigned int reading[] = {
      1001, 1027, 1187, 1267, 1347, 1427, 1507, 1587, 1667, 1748, 1874,
      2015, 2143, 2223, 2351, 2478, 2606, 2734, 2813, 2906, 2987, 3067,
      3147, 3227, 3307, 3387, 3466, 3607, 3735, 3815, 3943, 4023, 4103,
      4231, 4357, 4495, 4575, 4703, 4831, 4911, 4991, 5119, 5198};

  dht22_t data;
  int status = dht22_convert(reading, &data);

  switch (status) {
  case 0:
    printf("Humidity:                    %d%\n\r", data.humidity);
    printf("Temperature(Degree Celsius): %d\n\r", data.temperature);
    break;
  case -2:
    printf("Parity checksum mismatch!");

    break;
  }

  return 0;
}
