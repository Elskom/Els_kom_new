#pragma once
#include <importlib.h>
#include <importlib_external.h>
#include "pyeimport.h"

static const struct _frozen _PyImport_FrozenModules[] = {
    /* importlib */
    {"_frozen_importlib", _Py_M__importlib, (int)sizeof(_Py_M__importlib)},
    {"_frozen_importlib_external", _Py_M__importlib_external,
        (int)sizeof(_Py_M__importlib_external)},
    /* pye import hook. */
    {"pyeimport", M_pyeimport, (int)sizeof(M_pyeimport)},
    {0, 0, 0} /* sentinel */
};

