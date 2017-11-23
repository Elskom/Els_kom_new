/*
    encryption.c
*/
#include <Python.h>
#include "encryption.h"

void make_string(int IntArray[], const int size, char *_str) {
  for (int i = 0; i < size; i++)
    _str[i] = (char)IntArray[i];
}

void get_encryption_key(char _key[]) {
  /* Note: this is not the real encryption
   * key for releases. The real one is
   * private.
   */
  int _key_string[] = {
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0
  };
  make_string(_key_string, 114, _key);
}

int PyEncryptionExec(const char *scriptstr) {
  char _codebuffer[1100];
  char _key[114] = {0};
  get_encryption_key(_key);
  snprintf(_codebuffer, 1100, scriptstr, _key);
  return PyRun_SimpleString(_codebuffer);
}
