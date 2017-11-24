/*
    encryption.cpp
*/
#include <Python.h>
#include "encryption.h"
#if __has_include("encryption_private.h")
/* we have our private release key. */
#include "encryption_private.h"
#else
/* we fall back to a null key. */
#define KEY_STRING \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0, 0, 0, 0, \
0, 0, 0, 0
#endif

void make_string(int IntArray[], const int size, char *_str) {
  for (int i = 0; i < size; i++)
    _str[i] = (char)IntArray[i];
}

void get_encryption_key(char _key[]) {
  int _key_string[] = {KEY_STRING};
  make_string(_key_string, 114, _key);
}

extern "C" int PyEncryptionExec(const char *scriptstr) {
  char _codebuffer[1100];
  char _key[114] = {0};
  get_encryption_key(_key);
  snprintf(_codebuffer, 1100, scriptstr, _key);
  return PyRun_SimpleString(_codebuffer);
}
