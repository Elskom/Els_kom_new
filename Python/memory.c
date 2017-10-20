/*
 * memory.c
 */
#include <Python.h>

PyMODINIT_FUNC PyInit__memimporter(void);

void PyImport_Memory() {
  // barrowed from py2exe.
  PyImport_AppendInittab("_memimporter", PyInit__memimporter);
}
