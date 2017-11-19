import types
import sys
import zipimport
import zlib
import base64
import os
import aescipher


__all__ = ['PyeZipImporter',
           'install', 'uninstall']


class PyeZipImportError(ImportError):
    pass


class PyeZipImporter(zipimport.zipimporter):
    _suffixes = ['.py', '.pyc', '.pye']
    _key = None

    def find_loader(self, fullname):
        loader, portions = zipimport.zipimporter.find_loader(self, fullname)
        if loader is None:
            pathname = fullname.replace(".", "\\")
            for s in self._suffixes:
                if (pathname + s) in self._files:
                    return self, []
            return None, []
        return loader, portions

    def find_module(self, fullname, path=None):
        result = zipimport.zipimporter.find_module(self, fullname, path)
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
            return zipimport.zipimporter.load_module(self, fullname)
        except ImportError:
            pass
        _encryptor = aescipher.AESCipher(self._key)
        filename = fullname.replace(".", "\\")
        suffixes = self._suffixes
        for s in suffixes:
            path = filename + s
            if path in self._files:
                if path.endswith('.pye'):
                    zlib_output = zlib.decompress(self.get_data(path))
                    base64_output = base64.b64decode(zlib_output)
                    decrypted_output = _encryptor.decrypt(base64_output)
                    if decrypted_output is not None:
                        module_data = decrypted_output
                        mod = types.ModuleType(fullname)
                        exec(module_data, mod.__dict__)
                        sys.modules[fullname] = mod
                        mod.__file__ = os.path.join(self.archive, path)
                        mod.__loader__ = self
                        return mod
        raise PyeZipImportError(f"can't find module {str(fullname)}")

    def __repr__(self):
        return "<%s object %r>" % (self.__class__.__name__, self.archive)


def install(encryption_key):
    PyeZipImporter._key = encryption_key
    sys.path_hooks.insert(0, PyeZipImporter)
    sys.path_importer_cache.clear()


def uninstall():
    sys.path_hooks.remove(PyeZipImporter)
    sys.path_importer_cache.clear()
