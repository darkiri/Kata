#include <stdio.h>

int atoi(const char* a) {
  int i = 0; char ch;
  while(ch = *a++) {
    i = i*10 + ch - '0';
  }
  return i;
}

void assert_equals(int expected, int actual) {
  if (expected != actual) {
    printf("%i != %i\n", expected, actual);
  } else {
    printf("ok\n");
  }
}

int main() {
  assert_equals(0, atoi("0"));
  assert_equals(1, atoi("1"));
  assert_equals(10, atoi("10"));
  assert_equals(19, atoi("19"));
  return 0;
}
