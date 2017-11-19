from Cython.Build import cythonize


cythonize('pyeimporter.py', build_dir="../PC")
cythonize('aescipher.py', build_dir="../PC")
