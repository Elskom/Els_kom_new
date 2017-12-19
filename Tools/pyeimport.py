import sys
try:
    import _zipimport as zipimport
except ImportError:
    import zipimport
import zlib
try:
    import _base64 as base64
except ImportError:
    import base64
try:
    # avoid importing entire
    # os module on Windows
    # just for urandom.
    import nt as os
except ImportError:
    # on non-windows systems
    # I think that urandom is
    # in the posix module.
    import posix as os
import _sha256
import aes


__all__ = ['PyeZipImporter', 'install', 'uninstall']


class AESCipher:
    def __init__(self, key):
        self.key = _sha256.sha256(key.encode()).digest()

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

    def decrypt(self, enc):
        iv = bytearray(enc[:16])
        cipher = aes.AES(mode='cbc', key=self.key, iv=iv)
        data = bytearray(enc[16:])
        cipher.decrypt(data)
        while data[len(data) - 1] == 0x00:
            del data[len(data) - 1]
        return bytes(data)


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
        _encryptor = AESCipher(self._key)
        filename = fullname.replace(".", "\\")
        suffixes = self._suffixes
        for s in suffixes:
            path = filename + s
            if path in self._files:
                if path.endswith('.pye'):
                    zlib_output = zlib.decompress(self.get_data(path))
                    decrypted_output = _encryptor.decrypt(zlib_output)
                    if decrypted_output is not None:
                        base64_output = base64.b64decode(decrypted_output)
                        zlib_output = zlib.decompress(base64_output)
                        module_data = zlib_output.decode('utf-8')
                        mod = type(sys)(fullname)
                        exec(module_data, mod.__dict__)
                        sys.modules[fullname] = mod
                        mod.__file__ = self.archive + "\\" + path
                        mod.__loader__ = self
                        return mod
        raise PyeZipImportError(f"can't find module {str(fullname)}")

    def __repr__(self):
        return "<PyeZipImporter object %r>" % self.archive


def install(encryption_key):
    PyeZipImporter._key = encryption_key
    sys.path_hooks.insert(0, PyeZipImporter)
    sys.path_importer_cache.clear()


def uninstall():
    sys.path_hooks.remove(PyeZipImporter)
    sys.path_importer_cache.clear()
