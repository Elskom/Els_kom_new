#include "../externals/zlib/zlib.h"


#if defined(ZLIB_VERNUM) && ZLIB_VERNUM >= 0x1221
#  define AT_LEAST_ZLIB_1_2_2_1
#endif

/* The following parameters are copied from zutil.h, version 0.95 */
#define DEFLATED   8
#if MAX_MEM_LEVEL >= 8
#  define DEF_MEM_LEVEL 8
#else
#  define DEF_MEM_LEVEL  MAX_MEM_LEVEL
#endif

/* Minimum value between x and y */
#define MemZlib_MIN(x, y) (((x) > (y)) ? (y) : (x))

/* Largest positive value of type size_t. */
#define MemZlib_SSIZE_T_MAX (((size_t)-1)>>1))
/* Smallest negative value of type size_t. */
#define MemZlib_SSIZE_T_MIN (-MemZlib_SSIZE_T_MAX-1)

struct MemZlib_buffer {
    Byte *buf;  // buffer data.
    size_t len;  // data length.
};

/* Initial buffer size. */
#define DEF_BUF_SIZE (16*1024)

static void*
PyZlib_Malloc(voidpf ctx, uInt items, uInt size)
{
    if (items > (size_t)MemZlib_SSIZE_T_MAX / size)
        return NULL;
    /* PyMem_Malloc() cannot be used: the GIL is not held when
       inflate() and deflate() are called */
    return PyMem_RawMalloc(items * size);
}

static void
PyZlib_Free(voidpf ctx, void *ptr)
{
    PyMem_RawFree(ptr);
}

static void
arrange_input_buffer(z_stream *zst, size_t *remains)
{
    zst->avail_in = MemZlib_MIN(*remains, UINT_MAX);
    *remains -= zst->avail_in;
}

static size_t
arrange_output_buffer_with_maximum(z_stream *zst, Byte *buffer,
                                   size_t length,
                                   size_t max_length)
{
    size_t occupied;

    if (*buffer == NULL) {
        if (!(*buffer = PyBytes_FromStringAndSize(NULL, length)))
            return -1;
        occupied = 0;
    }
    else {
        occupied = zst->next_out - (Byte *)PyBytes_AS_STRING(*buffer);

        if (length == occupied) {
            z_size_t new_length;
            assert(length <= max_length);
            /* can not scale the buffer over max_length */
            if (length == max_length)
                return -2;
            if (length <= (max_length >> 1))
                new_length = length << 1;
            else
                new_length = max_length;
            if (_PyBytes_Resize(buffer, new_length) < 0)
                return -1;
            length = new_length;
        }
    }

    zst->avail_out = MemZlib_MIN(length - occupied, UINT_MAX);
    zst->next_out = (Byte *)PyBytes_AS_STRING(*buffer) + occupied;

    return length;
}

static size_t
arrange_output_buffer(z_stream *zst, Byte *buffer, size_t length)
{
    size_t ret;

    ret = arrange_output_buffer_with_maximum(zst, buffer, length,
                                             MemZlib_SSIZE_T_MAX);
    //if (ret == -2)
        //PyErr_NoMemory();

    return ret;
}

/*[clinic input]
zlib.compress

    data: Py_buffer
        Binary data to be compressed.
    /
    level: int(c_default="Z_DEFAULT_COMPRESSION") = Z_DEFAULT_COMPRESSION
        Compression level, in 0-9 or -1.

Returns a bytes object containing compressed data.
[clinic start generated code]*/

static PyObject *
zlib_compress_impl(MemZlib_buffer data, int level)
/*[clinic end generated code: output=d80906d73f6294c8 input=638d54b6315dbed3]*/
{
    PyObject *RetVal = NULL;
    Byte *ibuf;
    size_t ibuflen, obuflen = DEF_BUF_SIZE;
    int err, flush;
    z_stream zst;

    ibuf = data->buf;
    ibuflen = data->len;

    zst.opaque = NULL;
    zst.zalloc = PyZlib_Malloc;
    zst.zfree = PyZlib_Free;
    zst.next_in = ibuf;
    err = deflateInit(&zst, level);

    switch (err) {
    case Z_OK:
        break;
    case Z_MEM_ERROR:
        //PyErr_SetString(PyExc_MemoryError,
        //                "Out of memory while compressing data");
        // TODO: Implement an error part.
        //goto error;
    case Z_STREAM_ERROR:
        //PyErr_SetString(ZlibError, "Bad compression level");
        //goto error;
        // TODO: Implement Error message here.
    default:
        deflateEnd(&zst);
        //zlib_error(zst, err, "while compressing data");
        // TODO: Implement Error message here.
        //goto error;
    }

    do {
        arrange_input_buffer(&zst, &ibuflen);
        flush = ibuflen == 0 ? Z_FINISH : Z_NO_FLUSH;

        do {
            obuflen = arrange_output_buffer(&zst, &RetVal, obuflen);
            if (obuflen < 0) {
                deflateEnd(&zst);
                //goto error;
                // TODO: Implement Error message here.
            }

//            Py_BEGIN_ALLOW_THREADS
            err = deflate(&zst, flush);
//            Py_END_ALLOW_THREADS

            if (err == Z_STREAM_ERROR) {
                deflateEnd(&zst);
                //zlib_error(zst, err, "while compressing data");
                //goto error;
                // TODO: Implement an error message here.
            }

        } while (zst.avail_out == 0);
        assert(zst.avail_in == 0);

    } while (flush != Z_FINISH);
    assert(err == Z_STREAM_END);

    err = deflateEnd(&zst);
    if (err == Z_OK) {
        //if (_PyBytes_Resize(&RetVal, zst.next_out -
        //                    (Byte *)PyBytes_AS_STRING(RetVal)) < 0)
        //    goto error;
        return RetVal;
    }
    // TODO: Implement Error message here.
    //else
    //    zlib_error(zst, err, "while finishing compression");
// error:
//    Py_XDECREF(RetVal);
    return NULL;
}

/*[clinic input]
zlib.decompress

    data: Py_buffer
        Compressed data.
    /
    wbits: int(c_default="MAX_WBITS") = MAX_WBITS
        The window buffer size and container format.
    bufsize: ssize_t(c_default="DEF_BUF_SIZE") = DEF_BUF_SIZE
        The initial output buffer size.

Returns a bytes object containing the uncompressed data.
[clinic start generated code]*/

static PyObject *
zlib_decompress_impl(PyObject *module, MemZlib_buffer data, int wbits,
                     size_t bufsize)
/*[clinic end generated code: output=77c7e35111dc8c42 input=21960936208e9a5b]*/
{
    PyObject *RetVal = NULL;
    Byte *ibuf;
    size_t ibuflen;
    int err, flush;
    z_stream zst;

    if (bufsize < 0) {
        //PyErr_SetString(PyExc_ValueError, "bufsize must be non-negative");
        // TODO: Implement Error here.
        return NULL;
    } else if (bufsize == 0) {
        bufsize = 1;
    }

    ibuf = data->buf;
    ibuflen = data->len;

    zst.opaque = NULL;
    zst.zalloc = PyZlib_Malloc;
    zst.zfree = PyZlib_Free;
    zst.avail_in = 0;
    zst.next_in = ibuf;
    err = inflateInit2(&zst, wbits);

    switch (err) {
    case Z_OK:
        break;
    case Z_MEM_ERROR:
        PyErr_SetString(PyExc_MemoryError,
                        "Out of memory while decompressing data");
        goto error;
    default:
        inflateEnd(&zst);
        zlib_error(zst, err, "while preparing to decompress data");
        goto error;
    }

    do {
        arrange_input_buffer(&zst, &ibuflen);
        flush = ibuflen == 0 ? Z_FINISH : Z_NO_FLUSH;

        do {
            bufsize = arrange_output_buffer(&zst, &RetVal, bufsize);
            if (bufsize < 0) {
                inflateEnd(&zst);
                goto error;
            }

//            Py_BEGIN_ALLOW_THREADS
            err = inflate(&zst, flush);
//            Py_END_ALLOW_THREADS

            switch (err) {
            case Z_OK:            /* fall through */
            case Z_BUF_ERROR:     /* fall through */
            case Z_STREAM_END:
                break;
            case Z_MEM_ERROR:
                inflateEnd(&zst);
                PyErr_SetString(PyExc_MemoryError,
                                "Out of memory while decompressing data");
                goto error;
            default:
                inflateEnd(&zst);
                zlib_error(zst, err, "while decompressing data");
                goto error;
            }

        } while (zst.avail_out == 0);

    } while (err != Z_STREAM_END && ibuflen != 0);


    if (err != Z_STREAM_END) {
        inflateEnd(&zst);
        zlib_error(zst, err, "while decompressing data");
        goto error;
    }

    err = inflateEnd(&zst);
    if (err != Z_OK) {
        zlib_error(zst, err, "while finishing decompression");
        goto error;
    }

    if (_PyBytes_Resize(&RetVal, zst.next_out -
                        (Byte *)PyBytes_AS_STRING(RetVal)) < 0)
        goto error;

    return RetVal;

 error:
    Py_XDECREF(RetVal);
    return NULL;
}

/*[clinic input]
zlib.adler32

    data: Py_buffer
    value: unsigned_int(bitwise=True) = 1
        Starting value of the checksum.
    /

Compute an Adler-32 checksum of data.

The returned checksum is an integer.
[clinic start generated code]*/

unsigned int
zlib_adler32_impl(MemZlib_buffer data, unsigned int value)
/*[clinic end generated code: output=422106f5ca8c92c0 input=6ff4557872160e88]*/
{
    /* Releasing the GIL for very small buffers is inefficient
       and may lower performance */
    if (data->len > 1024*5) {
        unsigned char *buf = data->buf;
        size_t len = data->len;

//        Py_BEGIN_ALLOW_THREADS
        /* Avoid truncation of length for very large buffers. adler32() takes
           length as an unsigned int, which may be narrower than size_t. */
        while (len > UINT_MAX) {
            value = adler32(value, buf, UINT_MAX);
            buf += (size_t) UINT_MAX;
            len -= (size_t) UINT_MAX;
        }
        value = adler32(value, buf, (unsigned int)len);
//        Py_END_ALLOW_THREADS
    } else {
        value = adler32(value, data->buf, (unsigned int)data->len);
    }
    return value & 0xffffffffU;
}

/*[clinic input]
zlib.crc32

    data: Py_buffer
    value: unsigned_int(bitwise=True) = 0
        Starting value of the checksum.
    /

Compute a CRC-32 checksum of data.

The returned checksum is an integer.
[clinic start generated code]*/

unsigned int
zlib_crc32_impl(MemZlib_buffer data, unsigned int value)
/*[clinic end generated code: output=63499fa20af7ea25 input=26c3ed430fa00b4c]*/
{
    int signed_val;

    /* Releasing the GIL for very small buffers is inefficient
       and may lower performance */
    if (data->len > 1024*5) {
        unsigned char *buf = data->buf;
        size_t len = data->len;

//        Py_BEGIN_ALLOW_THREADS
        /* Avoid truncation of length for very large buffers. crc32() takes
           length as an unsigned int, which may be narrower than size_t. */
        while (len > UINT_MAX) {
            value = crc32(value, buf, UINT_MAX);
            buf += (size_t) UINT_MAX;
            len -= (size_t) UINT_MAX;
        }
        signed_val = crc32(value, buf, (unsigned int)len);
//        Py_END_ALLOW_THREADS
    } else {
        signed_val = crc32(value, data->buf, (unsigned int)data->len);
    }
    return signed_val & 0xffffffffU;
}
