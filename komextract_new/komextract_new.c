/*
    komextract_new.c

    Enxtrypoint of the embeded python interpreter for running
    code to extract from kom files.

    Note: This code is for WIN32/WIN64 only.
*/
#include <Python.h>
/*
  for loading python script from the
  resource section.
*/
#include <windows.h>
#include "resource.h"

int
wmain(int argc, wchar_t *argv[])
{
  int err;
  wchar_t *program = Py_DecodeLocale(argv[0], NULL);
  wchar_t **argv_copy = argv;
  if (program == NULL) {
    fprintf(stderr, "Fatal error: cannot decode argv[0]\n");
    exit(1);
  }
  Py_SetProgramName(program);  /* optional but recommended */
  Py_Initialize();
  int initialized = Py_IsInitialized();
  if (initialized != 0) {
    /*
      allows use of sys.argv to be possible
      with no tracebacks.
    */
    argv_copy[0] = L"";
    PySys_SetArgvEx(argc, argv_copy, 0);
    HRSRC script_resource = FindResource(
      NULL, MAKEINTRESOURCE(IDR_RCDATA1), RT_RCDATA);
    unsigned int script_size = SizeofResource(NULL, script_resource);
    HGLOBAL main_script = LoadResource(NULL, script_resource);
    void* pmain_script = LockResource(main_script);
    if (script_size >= 0) {
      err = PyRun_SimpleString((const char *)pmain_script);
    } else {
      /* python script is empty. */
      fprintf(stderr, "Fatal error: Python script is empty.\n");
    }
    if (err > 0)
      PyErr_Print();
    Py_Finalize();
  } else {
    /* python is not initialized. */
    fprintf(stderr, "Fatal error: Python is not initialized.\n");
  }
  PyMem_RawFree(program);
  return 0;
}
