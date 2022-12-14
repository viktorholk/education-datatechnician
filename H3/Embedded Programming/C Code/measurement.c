#include <stdint.h>
#include <stdio.h>

void printbits(unsigned int v) {
  for (; v; v >>= 1)
    putchar('0' + (v & 1));

  printf("\n");
}

int main(void) {

  //int reading[] = {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0,
  //                 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0};

  //unsigned char bytes[4];

  //for (int i = 0; i < 4; i++) {

  //  for (int j = 7; j >= 0; j--) {
  //    bytes[i] |= (reading[8 * i + j] << j);
  //  }
  //}

  //for (int i = 0; i < 4; i++) {
  //  printf("%x\n", bytes[i]);
  //}

  int bits[] = {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0};

  // Define an array to hold the resulting bytes
  unsigned char bytes[4];

  // Iterate over the bytes in the array
  for (int i = 0; i < 4; i++) {
    // Iterate over the bits in the byte
    for (int j = 7; j >= 0; j--) {
      // Set the j-th bit of the i-th byte to the value of the (8 * i + j)-th
      // bit in the array
      bytes[i] |= (bits[8 * i + j] << j);
    }
  }

  // Print the resulting bytes
  for (int i = 0; i < 4; i++) {
    printf("%x ", bytes[i]); // Output: 00 d0 00 00
  }

  // int r[] = {1, 0, 1, 0, 0, 1, 1, 0};

  // unsigned char byte = 0;

  // for (int i = 7; i >= 0; i--) {
  //   // Set the i-th bit of the byte to the value of the i-th bit in the array
  //   byte |= (r[i] << (7 - i));
  // }

  // printf("%d", byte);
  return 0;
}
