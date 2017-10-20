"""
Encrypted Python Script Importer.
"""
from importlib.machinery import EXTENSION_SUFFIXES
from types import ModuleType
import sys
from zipimport import zipimporter, ZipImportError
from zlib import decompress
from base64 import b64decode
import os
import hashlib

from Crypto.Cipher import AES
from Crypto import Random
try:
    import _memimporter
except ImportError:
    _memimporter = None  # no memory .pyd or dll importer for zips.


__all__ = ['AESCipher', 'PyeZipImporter',
           'install', 'uninstall']


def _register_key(encryption_key):
    PyeZipImporter._key = encryption_key
    if _memimporter is not None:
        for ext in EXTENSION_SUFFIXES:
            # import extension modules from zip.
            PyeZipImporter._suffixes.append(ext)


class AESCipher:
    def __init__(self, key): 
        self.bs = 32
        self.key = hashlib.sha256(key.encode()).digest()

    def encrypt(self, raw):
        raw = self._pad(raw)
        iv = Random.new().read(AES.block_size)
        cipher = AES.new(self.key, AES.MODE_CBC, iv)
        return iv + cipher.encrypt(raw)

    def decrypt(self, enc):
        """returns decrpyted content, returns None on failure."""
        iv = enc[:AES.block_size]
        cipher = AES.new(self.key, AES.MODE_CBC, iv)
        try:
            return self._unpad(cipher.decrypt(enc[AES.block_size:])).decode('utf-8')
        except UnicodeDecodeError:
            return None

    def _pad(self, s):
        return s + (self.bs - len(s) % self.bs) * chr(self.bs - len(s) % self.bs)

    def _unpad(self, s):
        type(self)
        return s[:-ord(s[len(s)-1:])]


class PyeZipImporter(zipimporter):
    _suffixes = ['.py', '.pyc', '.pye']
    _key = None

    def find_loader(self, fullname):
        loader, portions = super().find_loader(fullname)
        if loader is None:
            pathname = fullname.replace(".", "\\")
            for s in self._suffixes:
                if (pathname + s) in self._files:
                    return self, []
            return None, []
        return loader, portions

    def find_module(self, fullname, path=None):
        result = zipimporter.find_module(self, fullname, path)
        if result:
            return result
        fullname = fullname.replace(".", "\\")
        for s in self._suffixes:
            if (fullname + s) in self._files:
                return self
        return None

    def load_module(self, fullname):
        if fullname in sys.modules:
            mod = sys.modules[fullname]
            return mod
        try:
            return zipimporter.load_module(self, fullname)
        except ImportError:
            pass
        _encryptor = AESCipher(self._key)
        filename = fullname.replace(".", "\\")
        suffixes = self._suffixes
        for s in suffixes:
            path = filename + s
            if path in self._files:
                if path.endswith('.pye'):
                    zlib_output = decompress(self.get_data(path))
                    decrypted_output = _encryptor.decrypt(zlib_output)
                    if decrypted_output is not None:
                        base64_output = b64decode(decrypted_output)
                        zlib_output = decompress(base64_output)
                        file2 = open(os.path.join(sys.path[0], filename + ".tmp"), 'wb')
                        file2.write(zlib_output)
                        file2.close()
                        file3 = open(os.path.join(sys.path[0], filename + ".tmp"), 'r')
                        module_data = file3.read()
                        file3.close()
                        os.remove(os.path.join(sys.path[0], filename + ".tmp"))
                        mod = ModuleType(fullname)
                        exec(module_data, mod.__dict__)
                        sys.modules[fullname] = mod
                        mod.__file__ = os.path.join(self.archive, path)
                        mod.__loader__ = self
                        return mod
                # only use this if we need to use the zipextension module stuff.
                if path.endswith('.pyd'):
                    if _memimporter is not None:
                        verbose = _memimporter.get_verbose_flag()
                        if sys.version_info >= (3, 0):
                            # name of initfunction
                            initname = "PyInit_" + fullname.split(".")[-1]
                        else:
                            # name of initfunction
                            initname = "init" + fullname.split(".")[-1]
                        if filename in ("pywintypes", "pythoncom"):
                            filename = filename + "%d%d" % sys.version_info[:2]
                            suffixes = ('.dll',)
                        if verbose > 1:
                            sys.stderr.write("# found %s in zipfile %s\n"
                                             % (path, self.archive))
                        mod = _memimporter.import_module(fullname, path,
                                                         initname,
                                                         self.get_data)
                        mod.__file__ = os.path.join(self.archive, path)
                        mod.__loader__ = self
                        if verbose:
                            sys.stderr.write("import %s # loaded from zipfile %s\n"
                                             % (fullname, mod.__file__))
                        return mod
        raise ZipImportError(f"can't find module {str(fullname)}")

    def __repr__(self):
        return "<%s object %r>" % (self.__class__.__name__, self.archive)


def install(encryption_key):
    "Install the PyeZipImporter"
    _register_key(encryption_key)
    sys.path_hooks.insert(0, PyeZipImporter)
    sys.path_importer_cache.clear()


def uninstall():
    "Uninstall the PyeZipImporter"
    sys.path_hooks.remove(PyeZipImporter)
    sys.path_importer_cache.clear()
