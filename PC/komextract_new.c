/*
 * komextract_new.c
 *
 * Entrypoint of the embeded python interpreter for
 * running code to extract from kom files.
 *
 * Note: This code is for WIN32/WIN64 only.
 */
#include <Python.h>
/* for loading python script from the resource
 * section.
 */
#include <windows.h>
#define WITH_ENCRYPTION
#ifdef WITH_ENCRYPTION
#include "encryption.h"
#endif
#include "resource.h"

#ifdef WITH_ENCRYPTION
PyMODINIT_FUNC PyInit_aes(void);
#endif

int
wmain(int argc, wchar_t **argv)
{
  int err;
  wchar_t *program = Py_DecodeLocale((char *)argv[0], NULL);
  wchar_t **argv_copy = argv;
  if (program == NULL) {
    LPSTR buffer1[36];
    LoadStringA(GetModuleHandle(NULL), IDS_STRING1, (LPSTR)buffer1, 36);
    fprintf(stderr, (const char *const)buffer1);
    exit(1);
  }
#ifdef WITH_ENCRYPTION
  PyImport_AppendInittab("aes", PyInit_aes);
#endif
  Py_SetProgramName(program);  /* optional but recommended */
  Py_Initialize();
  int initialized = Py_IsInitialized();
  if (initialized != 0) {
    /* allows use of sys.argv to be possible
     * with no tracebacks.
     */
    argv_copy[0] = L"";
    PySys_SetArgvEx(argc, argv_copy, 0);
#ifdef WITH_ENCRYPTION
    HRSRC script2_resource = FindResource(
      NULL, MAKEINTRESOURCE(IDR_RCDATA2), RT_RCDATA);
    unsigned int script2_size = SizeofResource(NULL, script2_resource);
    HGLOBAL main2_script = LoadResource(NULL, script2_resource);
    void* pmain2_script = LockResource(main2_script);
    if (script2_size >= 0) {
      PyEncryptionExec((const char *)pmain2_script);
    } else {
      /* python script is empty. */
      LPSTR buffer4[62];
      LoadStringA(GetModuleHandle(NULL), IDS_STRING4, (LPSTR)buffer4, 62);
      fprintf(stderr, (const char *const)buffer4);
    }
#endif
    HRSRC script_resource = FindResource(
      NULL, MAKEINTRESOURCE(IDR_RCDATA1), RT_RCDATA);
    unsigned int script_size = SizeofResource(NULL, script_resource);
    HGLOBAL main_script = LoadResource(NULL, script_resource);
    void* pmain_script = LockResource(main_script);
    if (script_size >= 0) {
      err = PyRun_SimpleString((const char *)pmain_script);
    } else {
      /* python script is empty. */
      LPSTR buffer2[38];
      LoadStringA(GetModuleHandle(NULL), IDS_STRING2, (LPSTR)buffer2, 38);
      fprintf(stderr, (const char *const)buffer2);
    }
    if (err > 0)
      PyErr_Print();
    Py_Finalize();
  } else {
    /* python is not initialized. */
    LPSTR buffer3[41];
    LoadStringA(GetModuleHandle(NULL), IDS_STRING3, (LPSTR)buffer3, 41);
    fprintf(stderr, (const char *const)buffer3);
  }
  PyMem_RawFree(program);
  return 0;
}
