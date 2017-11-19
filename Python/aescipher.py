#!python
#cython: language_level=3
import hashlib
import os
import aes


class AESCipher:
    def __init__(self, key):
        self.key = hashlib.sha256(key.encode()).digest()

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
