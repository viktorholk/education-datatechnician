#include <stdint.h>
#include <stdio.h>

void printbits(unsigned int v) {
  for (; v; v >>= 1)
    putchar('0' + (v & 1));

  printf("\n");
}

int main(void) {

  int bits[] = {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0};

  // Define an array to hold the resulting bytes
  unsigned char bytes[5] = {0x00, 0x00, 0x00, 0x00, 0x00};

  // Iterate over the bytes in the array
  for (int i = 0; i < 5; i++) {
    for (int j = 0; j < 8; j++) {
      bytes[i] |= (bits[8 * i + (7 - j)] << j);
    }
  }

  // Print the resulting bytes
  for (int i = 0; i < 5; i++) {
    printf("%d ", bytes[i]);
  }

  return 0;
}
