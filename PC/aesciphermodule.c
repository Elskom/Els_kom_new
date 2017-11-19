/*
aesciphermodule.c
*/
#include <python.h>
#include <structmember.h>
PyTypeObject AESCipher;
PyObject *hashlib;
PyObject *os;
PyObject *aes;

typedef struct {
  PyObject_HEAD
  PyObject *key;
} AESCipherObj;

/*
class AESCipher:
    def __init__(self, key):
        self.key = hashlib.sha256(key.encode()).digest()
*/
static int
AESCipher_init(AESCipherObj *self, PyObject *args)
{
  if (!PyArg_ParseTuple(args, "O", &self->key))
    return -1;
  PyObject *sha256 = PyObject_GetAttrString(hashlib, "sha256");
  PyObject *encode = PyObject_CallObject(PyObject_GetAttrString(self->key, "encode"), NULL);
  PyObject *result_hash = PyObject_CallObject(sha256, encode);
  PyObject *digest = PyObject_GetAttrString(result_hash, "digest");
  self->key = PyObject_CallObject(digest, NULL);
  /* hopefully these don't spark bugs!!! */
  Py_XDECREF(digest);
  Py_XDECREF(result_hash);
  Py_XDECREF(encode);
  Py_XDECREF(sha256);
  return 0;
}

/*
    def encrypt(self, raw):
        iv = bytearray(os.urandom(16))
        cipher = aes.AES(mode='cbc', key=self.key, iv=iv)
        # for when raw is an bytes object and not an str object.
        if isinstance(raw, str):
            data = bytearray(raw.encode('ascii'))
        else:
            data = bytearray(raw)
        while (len(data) / 16) % 1 != 0:
            data.append(0x00)
        cipher.encrypt(data)
        return bytes(iv + data)
*/
static PyObject *
AESCipher_encrypt(AESCipherObj* self, PyObject *args)
{
  PyObject *raw;
  PyObject *data;
  PyObject *iv;
  if (!PyArg_ParseTuple(args, "O", &raw))
    return -1;
  iv = PyByteArray_FromObject(PyObject_CallObject(PyObject_GetAttrString(os, "urandom"), Py_BuildValue("(i)", 16)));
  if (PyUnicode_CheckExact(raw)) {
    data = PyByteArray_FromObject(PyObject_CallObject(PyObject_GetAttrString(raw, "encode"), Py_BuildValue("(s)", "ascii")));
  } else {
    data = PyByteArray_FromObject(raw);
  }
  // is this while loop the same as the python one above???
  while ((PyObject_Length(data) / 16) % 1 != 0) {
    Py_XDECREF(PyObject_CallObject(PyObject_GetAttrString(data, "append"), Py_BuildValue("(i)", 0x00)));
  }
  PyObject *return_array = PyByteArray_Concat(iv, data);

  /* hopefully these don't spark bugs!!! */
  Py_XDECREF(PyObject_CallObject(PyObject_GetAttrString(PyObject_Call(PyObject_GetAttrString(aes, "AES"), PyTuple_New(0), Py_BuildValue("{s:s,s:O,s:O}", "mode", "cbc", "key", &self->key, "iv", &iv)), "encrypt"), data));
  Py_XDECREF(iv);
  Py_XDECREF(data);
  Py_XDECREF(raw);
  return PyBytes_FromObject(return_array);
}

/*
    def decrypt(self, enc):
        iv = bytearray(enc[:16])
        cipher = aes.AES(mode='cbc', key=self.key, iv=iv)
        data = bytearray(enc[16:])
        cipher.decrypt(data)
        while data[len(data) - 1] == 0x00:
            del data[len(data) - 1]
        return bytes(data)
*/
static PyObject *
AESCipher_decrypt(AESCipherObj* self, PyObject *args)
{
  PyObject *enc;
  if (!PyArg_ParseTuple(args, "O", &enc))
    return -1;
  PyObject *iv = PyByteArray_FromObject(PyObject_GetItem(enc, PySlice_New(NULL, PyLong_FromSsize_t((Py_ssize_t)16), NULL)));
  PyObject *data = PyByteArray_FromObject(PyObject_GetItem(enc, PySlice_New(PyLong_FromSsize_t((Py_ssize_t)16), NULL, NULL)));
  Py_XDECREF(PyObject_CallObject(PyObject_GetAttrString(PyObject_Call(PyObject_GetAttrString(aes, "AES"), PyTuple_New(0), Py_BuildValue("{s:s,s:O,s:O}", "mode", "cbc", "key", &self->key, "iv", &iv)), "decrypt"), data));
  // is this while loop the same as the python one above???
  while (PyObject_GetItem(data, PySlice_New(NULL, PyObject_Length(data) - 1, NULL)) == 0x00) {
    PySequence_DelItem(data, PyObject_Length(data) - 1);
  }

  /* hopefully these don't spark bugs!!! */
  Py_XDECREF(iv);
  Py_XDECREF(enc);
  return PyBytes_FromObject(data);
}

static PyMethodDef AESCipher_methods[] = {
  {"encrypt", (PyCFunction)AESCipher_encrypt, METH_VARARGS,
   NULL
  },
  {"decrypt", (PyCFunction)AESCipher_decrypt, METH_VARARGS,
   NULL
  },
  {NULL}
};

static PyMemberDef AESCipher_members[] = {
  {"key", T_OBJECT, offsetof(AESCipherObj, key), 0,
   NULL},
  {NULL}
};

PyTypeObject AESCipher = {
  PyVarObject_HEAD_INIT(NULL, 0)
  "aescipher.AESCipher",
  .tp_basicsize = sizeof(AESCipherObj),
  .tp_flags = Py_TPFLAGS_DEFAULT | Py_TPFLAGS_BASETYPE,
  .tp_methods = AESCipher_methods,
  .tp_members = AESCipher_members,
  .tp_init = (initproc)AESCipher_init,
  .tp_new = PyType_GenericNew,
};

static PyModuleDef aesciphermodule = {
  PyModuleDef_HEAD_INIT,
  "aescipher",
  NULL,
  -1,
  NULL, NULL, NULL, NULL, NULL
};

#if defined(SUPPRESS_INITFUNC_EXPORT)
#  undef PyMODINIT_FUNC
#  define PyMODINIT_FUNC PyObject *
#endif

/*
import hashlib
import os
import aes
*/
PyMODINIT_FUNC
PyInit_aescipher(void)
{
  PyObject* m;
  hashlib = PyImport_ImportModule("hashlib");
  os = PyImport_ImportModule("os");
  aes = PyImport_ImportModule("aes");
  if (PyType_Ready(&AESCipher) < 0)
    return NULL;
  m = PyModule_Create(&aesciphermodule);
  if (m == NULL)
    return NULL;
  Py_XINCREF(&AESCipher);
  PyModule_AddObject(m, "AESCipher", (PyObject *)&AESCipher);
  return m;
}
