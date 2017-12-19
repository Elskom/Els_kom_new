/*
 * Drop in Replacement to the
 * base64 Python Standard Library
 * module that does the same exact
 * thing.
 */
#include <Python.h>

/*
 * output 1 byte for every 2 input:
 *
 *               outputs: 1
 * inputs: 1 = ----1111 = 1111----
 *         2 = ----2222 = ----2222
 */

/* Decoding... */
static const size_t BASE16_INPUT = 2;
static const size_t BASE16_OUTPUT = 1;
static const size_t BASE16_MAX_PADDING = 0;
static const unsigned char BASE16_MAX_VALUE = 15;
static const unsigned char BASE16_TABLE[0x80] = {
    /*00-07*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*08-0f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*10-17*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*18-1f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*20-27*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*28-2f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*30-37*/ 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, /*8 = '0'-'7'*/
    /*38-3f*/ 0x08, 0x09, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, /*2 = '8'-'9'*/
    /*40-47*/ 0xFF, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0xFF, /*6 = 'A'-'F'*/
    /*48-4f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*50-57*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*58-5f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*60-67*/ 0xFF, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0xFF, /*6 = 'a'-'f' (same as 'A'-'F')*/
    /*68-6f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*70-77*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*78-7f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
};

/*
 * output 5 bytes for every 8 input:
 *
 *               outputs: 1        2        3        4        5
 * inputs: 1 = ---11111 = 11111---
 *         2 = ---222XX = -----222 XX------
 *         3 = ---33333 =          --33333-
 *         4 = ---4XXXX =          -------4 XXXX----
 *         5 = ---5555X =                   ----5555 X-------
 *         6 = ---66666 =                            -66666--
 *         7 = ---77XXX =                            ------77 XXX-----
 *         8 = ---88888 =                                     ---88888
 */

static const size_t BASE32_INPUT = 8;
static const size_t BASE32_OUTPUT = 5;
static const size_t BASE32_MAX_PADDING = 6;
static const unsigned char BASE32_MAX_VALUE = 31;
static const unsigned char BASE32_TABLE[ 0x80 ] = {
    /*00-07*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*08-0f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*10-17*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*18-1f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*20-27*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*28-2f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*30-37*/ 0xFF, 0xFF, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f, /*6 = '2'-'7'*/
    /*38-3f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x20, 0xFF, 0xFF, /*1 = '='*/
    /*40-47*/ 0xFF, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, /*7 = 'A'-'G'*/
    /*48-4f*/ 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, /*8 = 'H'-'O'*/
    /*50-57*/ 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, /*8 = 'P'-'W'*/
    /*58-5f*/ 0x17, 0x18, 0x19, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, /*3 = 'X'-'Z'*/
    /*60-67*/ 0xFF, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, /*7 = 'a'-'g' (same as 'A'-'G')*/
    /*68-6f*/ 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, /*8 = 'h'-'o' (same as 'H'-'O')*/
    /*70-77*/ 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, /*8 = 'p'-'w' (same as 'P'-'W')*/
    /*78-7f*/ 0x17, 0x18, 0x19, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF  /*3 = 'x'-'z' (same as 'X'-'Z')*/
};

/*
 * output 3 bytes for every 4 input:
 *
 *               outputs: 1        2        3
 * inputs: 1 = --111111 = 111111--
 *         2 = --22XXXX = ------22 XXXX----
 *         3 = --3333XX =          ----3333 XX------
 *         4 = --444444 =                   --444444
 */

static const size_t BASE64_INPUT = 4;
static const size_t BASE64_OUTPUT = 3;
static const size_t BASE64_MAX_PADDING = 2;
static const unsigned char BASE64_MAX_VALUE = 63;
static const unsigned char BASE64_TABLE[ 0x80 ] = {
    /*00-07*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*08-0f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*10-17*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*18-1f*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*20-27*/ 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
    /*28-2f*/ 0xFF, 0xFF, 0xFF, 0x3e, 0xFF, 0xFF, 0xFF, 0x3f, /*2 = '+' and '/'*/
    /*30-37*/ 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, /*8 = '0'-'7'*/
    /*38-3f*/ 0x3c, 0x3d, 0xFF, 0xFF, 0xFF, 0x40, 0xFF, 0xFF, /*2 = '8'-'9' and '='*/
    /*40-47*/ 0xFF, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, /*7 = 'A'-'G'*/
    /*48-4f*/ 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, /*8 = 'H'-'O'*/
    /*50-57*/ 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, /*8 = 'P'-'W'*/
    /*58-5f*/ 0x17, 0x18, 0x19, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, /*3 = 'X'-'Z'*/
    /*60-67*/ 0xFF, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f, 0x20, /*7 = 'a'-'g'*/
    /*68-6f*/ 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, /*8 = 'h'-'o'*/
    /*70-77*/ 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x30, /*8 = 'p'-'w'*/
    /*78-7f*/ 0x31, 0x32, 0x33, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF  /*3 = 'x'-'z'*/
};

/*
 * output 4 bytes for every 5 input
 */

static const size_t BASE85_INPUT = 5;
static const size_t BASE85_OUTPUT = 4;

#define FOLD_ZERO 1 /*output 'z' instead of '!!!!!'*/
//#define FOLD_SPACES 1 /*output 'y' instead of 4 spaces*/

/* Encoding... */

/*
 * output 2 bytes for every 1 input:
 *
 *                 inputs: 1
 * outputs: 1 = ----1111 = 1111----
 *          2 = ----2222 = ----2222
 */

static const size_t BASE16_ENC_INPUT = 1;
static const size_t BASE16_ENC_OUTPUT = 2;
static const char* const BASE16_ENC_TABLE = "0123456789ABCDEF";

/*
 * output 8 bytes for every 5 input:
 *
 *                 inputs: 1        2        3        4        5
 * outputs: 1 = ---11111 = 11111---
 *          2 = ---222XX = -----222 XX------
 *          3 = ---33333 =          --33333-
 *          4 = ---4XXXX =          -------4 XXXX----
 *          5 = ---5555X =                   ----5555 X-------
 *          6 = ---66666 =                            -66666--
 *          7 = ---77XXX =                            ------77 XXX-----
 *          8 = ---88888 =                                     ---88888
 */

static const size_t BASE32_ENC_INPUT = 5;
static const size_t BASE32_ENC_OUTPUT = 8;
static const char* const BASE32_ENC_TABLE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567=";

/*
 * output 4 bytes for every 3 input:
 *
 *                 inputs: 1        2        3
 * outputs: 1 = --111111 = 111111--
 *          2 = --22XXXX = ------22 XXXX----
 *          3 = --3333XX =          ----3333 XX------
 *          4 = --444444 =                   --444444
 */

static const size_t BASE64_ENC_INPUT = 3;
static const size_t BASE64_ENC_OUTPUT = 4;
static const char* const BASE64_ENC_TABLE = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

/*
 * output 5 bytes for every 4 input
 */

static const size_t BASE85_ENC_INPUT = 4;
static const size_t BASE85_ENC_OUTPUT = 5;

/* implementation... */
static int BaseXXValidateA(const char* src, size_t srcChars, size_t inputBytes,
    size_t maxPadding, unsigned char maxValue, const unsigned char table[])
{
    /*
     * returns 0 if the source is a valid baseXX encoding
     */

    if (!src)
        return -1; /*ERROR - NULL pointer*/

    if (srcChars % inputBytes != 0)
        return -3; /*ERROR - extra characters*/

    /* check the bytes */
    for (; srcChars >= 1; --srcChars, ++src)
    {
        unsigned char ch = *src;
        if ((ch >= 0x80) || (table[ch] > maxValue))
            break;
    }

    /* check any padding */
    for (; 1 <= srcChars && srcChars <= maxPadding; --srcChars, ++src)
    {
        unsigned char ch = *src;
        if ((ch >= 0x80) || (table[ch] != maxValue + 1))
            break;
    }

    /* if srcChars isn't zero then the encoded string isn't valid */
    if (srcChars != 0)
        return -2; /*ERROR - invalid baseXX character*/

    /* OK */
    return 0;
}

static size_t BaseXXDecodeGetLength(size_t srcChars, size_t inputBytes,
    size_t outputBytes)
{
    if (srcChars % inputBytes != 0)
        return 0; /*ERROR - extra characters*/

    /* OK */
    return (((srcChars + inputBytes - 1) / inputBytes) * outputBytes);
}

static size_t BaseXXEncodeGetLength(size_t srcBytes, size_t inputBytes, size_t outputBytes)
{
    return (((srcBytes + inputBytes - 1) / inputBytes) * outputBytes) + 1; /*plus terminator*/
}

/*
 * Returns: 0 = Success
 * -1 = Null pointer
 * -2 = Invalid character
 * -3 = Extra Characters
 */
static int Base16ValidateA(const char* src, size_t srcChars)
{
    return BaseXXValidateA(src, srcChars, BASE16_INPUT,
        BASE16_MAX_PADDING, BASE16_MAX_VALUE, BASE16_TABLE);
}

static size_t Base16DecodeGetLength(size_t srcChars)
{
    return BaseXXDecodeGetLength(srcChars, BASE16_INPUT, BASE16_OUTPUT);
}

static size_t Base16DecodeA(void* dest, const char* src, size_t srcChars)
{
    if (dest && src && (srcChars % BASE16_INPUT == 0))
    {
        const char* pSrc = src;
        unsigned char* pDest = (unsigned char*)dest;
        size_t dwSrcSize = srcChars;
        size_t dwDestSize = 0;
        unsigned char in1, in2;

        while (dwSrcSize >= 1)
        {
            /* 2 inputs */
            in1 = *pSrc++;
            in2 = *pSrc++;
            dwSrcSize -= BASE16_INPUT;

            /* Validate ASCII */
            if (in1 >= 0x80 || in2 >= 0x80)
                return 0; /*ERROR - invalid base16 character*/

            /* Convert ASCII to base16 */
            in1 = BASE16_TABLE[in1];
            in2 = BASE16_TABLE[in2];

            /* Validate base16 */
            if (in1 > BASE16_MAX_VALUE || in2 > BASE16_MAX_VALUE)
                return 0; /*ERROR - invalid base16 character*/

            /* 1 output */
            *pDest++ = ((in1 << 4) | in2);
            dwDestSize += BASE16_OUTPUT;
        }

        return dwDestSize;
    }
    else
        return 0; /*ERROR - null pointer, or srcChars isn't a multiple of 2*/
}

/*
 * Returns: 0 = Success
 * -1 = Null pointer
 * -2 = Invalid character
 * -3 = Extra Characters
 */
static int Base32ValidateA(const char* src, size_t srcChars)
{
    return BaseXXValidateA(src, srcChars, BASE32_INPUT,
        BASE32_MAX_PADDING, BASE32_MAX_VALUE, BASE32_TABLE);
}

static size_t Base32DecodeGetLength(size_t srcChars)
{
    return BaseXXDecodeGetLength(srcChars, BASE32_INPUT, BASE32_OUTPUT);
}

static size_t Base32DecodeA(void* dest, const char* src, size_t srcChars)
{
    if (dest && src && (srcChars % BASE32_INPUT == 0))
    {
        const char* pSrc = src;
        unsigned char* pDest = (unsigned char*)dest;
        size_t dwSrcSize = srcChars;
        size_t dwDestSize = 0;
        unsigned char in1, in2, in3, in4, in5, in6, in7, in8;

        while (dwSrcSize >= 1)
        {
            /* 8 inputs */
            in1 = *pSrc++;
            in2 = *pSrc++;
            in3 = *pSrc++;
            in4 = *pSrc++;
            in5 = *pSrc++;
            in6 = *pSrc++;
            in7 = *pSrc++;
            in8 = *pSrc++;
            dwSrcSize -= BASE32_INPUT;

            /* Validate ASCII */
            if (in1 >= 0x80 || in2 >= 0x80 || in3 >= 0x80 || in4 >= 0x80
                || in5 >= 0x80 || in6 >= 0x80 || in7 >= 0x80 || in8 >= 0x80)
                return 0; /*ERROR - invalid base32 character*/
            if (in1 == '=' || in2 == '=')
                return 0; /*ERROR - invalid padding*/
            if (dwSrcSize == 0)
            {
                if (in3 == '=' && in4 != '=')
                    return 0; /*ERROR - invalid padding*/
                if (in4 == '=' && in5 != '=')
                    return 0; /*ERROR - invalid padding*/
                if (in5 == '=' && in6 != '=')
                    return 0; /*ERROR - invalid padding*/
                if (in6 == '=' && in7 != '=')
                    return 0; /*ERROR - invalid padding*/
                if (in7 == '=' && in8 != '=')
                    return 0; /*ERROR - invalid padding*/
            }
            else
            {
                if (in3 == '=' || in4 == '=' || in5 == '=' || in6 == '=' || in7 == '=' || in8 == '=')
                    return 0; /*ERROR - invalid padding*/
            }

            /* Convert ASCII to base32 */
            in1 = BASE32_TABLE[in1];
            in2 = BASE32_TABLE[in2];
            in3 = BASE32_TABLE[in3];
            in4 = BASE32_TABLE[in4];
            in5 = BASE32_TABLE[in5];
            in6 = BASE32_TABLE[in6];
            in7 = BASE32_TABLE[in7];
            in8 = BASE32_TABLE[in8];

            /* Validate base32 */
            if (in1 > BASE32_MAX_VALUE || in2 > BASE32_MAX_VALUE)
                return 0; /*ERROR - invalid base32 character*/
            /*the following can be padding*/
            if (in3 > BASE32_MAX_VALUE + 1 || in4 > BASE32_MAX_VALUE + 1
                || in5 > BASE32_MAX_VALUE + 1 || in6 > BASE32_MAX_VALUE + 1
                || in7 > BASE32_MAX_VALUE + 1 || in8 > BASE32_MAX_VALUE + 1)
                return 0; /*ERROR - invalid base32 character*/

            /* 5 outputs */
            *pDest++ = ((in1 & 0x1f) << 3) | ((in2 & 0x1c) >> 2);
            *pDest++ = ((in2 & 0x03) << 6) | ((in3 & 0x1f) << 1) | ((in4 & 0x10) >> 4);
            *pDest++ = ((in4 & 0x0f) << 4) | ((in5 & 0x1e) >> 1);
            *pDest++ = ((in5 & 0x01) << 7) | ((in6 & 0x1f) << 2) | ((in7 & 0x18) >> 3);
            *pDest++ = ((in7 & 0x07) << 5) | (in8 & 0x1f);
            dwDestSize += BASE32_OUTPUT;

            /* Padding */
            if (in8 == BASE32_MAX_VALUE + 1)
            {
                --dwDestSize;
                assert((in7 == BASE32_MAX_VALUE + 1 && in6 == BASE32_MAX_VALUE + 1)
                    || (in7 != BASE32_MAX_VALUE + 1));
                if (in6 == BASE32_MAX_VALUE + 1)
                {
                    --dwDestSize;
                    if (in5 == BASE32_MAX_VALUE + 1)
                    {
                        --dwDestSize;
                        assert((in4 == BASE32_MAX_VALUE + 1 && in3 == BASE32_MAX_VALUE + 1)
                            || (in4 != BASE32_MAX_VALUE + 1));
                        if (in3 == BASE32_MAX_VALUE + 1)
                        {
                            --dwDestSize;
                        }
                    }
                }
            }
        }

        return dwDestSize;
    }
    else
        return 0; /*ERROR - null pointer, or srcChars isn't a multiple of 8*/
}

/*
 * Returns: 0 = Success
 * -1 = Null pointer
 * -2 = Invalid character
 * -3 = Extra Characters
 */
static int Base64ValidateA(const char* src, size_t srcChars)
{
    return BaseXXValidateA(src, srcChars, BASE64_INPUT,
        BASE64_MAX_PADDING, BASE64_MAX_VALUE, BASE64_TABLE);
}

static size_t Base64DecodeGetLength(size_t srcChars)
{
    return BaseXXDecodeGetLength(srcChars, BASE64_INPUT, BASE64_OUTPUT);
}

static size_t Base64DecodeA(void* dest, const char* src, size_t srcChars)
{
    if (dest && src && (srcChars % BASE64_INPUT == 0))
    {
        const char* pSrc = src;
        unsigned char* pDest = (unsigned char*)dest;
        size_t dwSrcSize = srcChars;
        size_t dwDestSize = 0;
        unsigned char in1, in2, in3, in4;

        while (dwSrcSize >= 1)
        {
            /* 4 inputs */
            in1 = *pSrc++;
            in2 = *pSrc++;
            in3 = *pSrc++;
            in4 = *pSrc++;
            dwSrcSize -= BASE64_INPUT;

            /* Validate ASCII */
            if (in1 >= 0x80 || in2 >= 0x80 || in3 >= 0x80 || in4 >= 0x80)
                return 0; /*ERROR - invalid base64 character*/
            if (in1 == '=' || in2 == '=')
                return 0; /*ERROR - invalid padding*/
            if (dwSrcSize == 0)
            {
                if (in3 == '=' && in4 != '=')
                    return 0; /*ERROR - invalid padding*/
            }
            else
            {
                if (in3 == '=' || in4 == '=')
                    return 0; /*ERROR - invalid padding*/
            }

            /* Convert ASCII to base64 */
            in1 = BASE64_TABLE[in1];
            in2 = BASE64_TABLE[in2];
            in3 = BASE64_TABLE[in3];
            in4 = BASE64_TABLE[in4];

            /* Validate base64 */
            if (in1 > BASE64_MAX_VALUE || in2 > BASE64_MAX_VALUE)
                return 0; /*ERROR - invalid base64 character*/
            /*the following can be padding*/
            if (in3 > BASE64_MAX_VALUE + 1 || in4 > BASE64_MAX_VALUE + 1)
                return 0; /*ERROR - invalid base64 character*/

            /* 3 outputs */
            *pDest++ = ((in1 & 0x3f) << 2) | ((in2 & 0x30) >> 4);
            *pDest++ = ((in2 & 0x0f) << 4) | ((in3 & 0x3c) >> 2);
            *pDest++ = ((in3 & 0x03) << 6) | (in4 & 0x3f);
            dwDestSize += BASE64_OUTPUT;

            /* Padding */
            if (in4 == BASE64_MAX_VALUE + 1)
            {
                --dwDestSize;
                if (in3 == BASE64_MAX_VALUE + 1)
                {
                    --dwDestSize;
                }
            }
        }

        return dwDestSize;
    }
    else
        return 0; /*ERROR - null pointer, or srcChars isn't a multiple of 4*/
}

/*
 * Returns: 0 = Success
 * -2 = Invalid character
 */
static int Base85ValidateA(const char* src, size_t srcChars)
{
    const char* pSrc = src;
    size_t dwSrcSize = srcChars;
    unsigned char in1, in2, in3, in4, in5;

    while (dwSrcSize >= 1)
    {
#if FOLD_ZERO
        if (*pSrc == 'z')
        {
            --dwSrcSize;
            continue;
        }
#endif

        /* 5 inputs */
        if (dwSrcSize >= 1)
        {
            in1 = (*pSrc++ - 33);
            --dwSrcSize;
        }
        in2 = in3 = in4 = in5 = 0;
        if (dwSrcSize >= 1)
        {
            in2 = (*pSrc++ - 33);
            --dwSrcSize;
        }
        if (dwSrcSize >= 1)
        {
            in3 = (*pSrc++ - 33);
            --dwSrcSize;
        }
        if (dwSrcSize >= 1)
        {
            in4 = (*pSrc++ - 33);
            --dwSrcSize;
        }
        if (dwSrcSize >= 1)
        {
            in5 = (*pSrc++ - 33);
            --dwSrcSize;
        }

        /* Validate */
        if (in1 >= 85 || in2 >= 85 || in3 >= 85 || in4 >= 85 || in5 >= 85)
            return -2; /*ERROR - invalid base85 character*/
    }

    /* OK */
    return 0;
}

static size_t Base85DecodeGetLength(size_t srcChars)
{
    return BaseXXDecodeGetLength(srcChars, BASE85_INPUT, BASE85_OUTPUT);
}

static size_t Base85DecodeA(void* dest, const char* src, size_t srcChars)
{
    if (dest && src && (srcChars % BASE85_INPUT == 0))
    {
        const char* pSrc = src;
        unsigned char* pDest = (unsigned char*)dest;
        size_t dwSrcSize = srcChars;
        size_t dwDestSize = 0;
        unsigned char in1, in2, in3, in4, in5;
        unsigned int out;

        while (dwSrcSize >= 1)
        {
#if FOLD_ZERO
            if (*pSrc == 'z')
            {
                ++pSrc;
                *pDest++ = 0;
                dwDestSize += BASE85_OUTPUT;
                continue;
            }
#endif

            /* 5 inputs */
            in1 = (*pSrc++ - 33);
            in2 = (*pSrc++ - 33);
            in3 = (*pSrc++ - 33);
            in4 = (*pSrc++ - 33);
            in5 = (*pSrc++ - 33);
            dwSrcSize -= BASE85_INPUT;

            /* Validate */
            if (in1 >= 85 || in2 >= 85 || in3 >= 85 || in4 >= 85 || in5 >= 85)
                return 0; /*ERROR - invalid base85 character*/

            /* Output */
            out = in1;
            out *= 85;
            out |= in2;
            out *= 85;
            out |= in3;
            out *= 85;
            out |= in4;
            out *= 85;
            out |= in5;
            *(unsigned int*)pDest = out;
            pDest += BASE85_OUTPUT;
            dwDestSize += BASE85_OUTPUT;
        }

        return dwDestSize;
    }
    else
        return 0; /*ERROR - null pointer, or srcChars isn't a multiple of 5*/
}

static size_t Base16EncodeGetLength(size_t srcBytes)
{
    return BaseXXEncodeGetLength(srcBytes, BASE16_ENC_INPUT, BASE16_ENC_OUTPUT);
}

static size_t Base16EncodeA(char* dest, const void* src, size_t srcBytes)
{
    if (dest && src)
    {
        unsigned char* pSrc = (unsigned char*)src;
        char* pDest = dest;
        size_t dwSrcSize = srcBytes;
        size_t dwDestSize = 0;
        unsigned char ch;

        while (dwSrcSize >= 1)
        {
            /* 1 input */
            ch = *pSrc++;
            dwSrcSize -= BASE16_ENC_INPUT;

            /* 2 outputs */
            *pDest++ = BASE16_ENC_TABLE[(ch & 0xf0) >> 4];
            *pDest++ = BASE16_ENC_TABLE[ch & 0x0f];
            dwDestSize += BASE16_ENC_OUTPUT;
        }
        *pDest++ = '\x0'; /*append terminator*/

        return dwDestSize;
    }
    else
        return 0; /*ERROR - null pointer*/
}

static size_t Base32EncodeGetLength(size_t srcBytes)
{
    return BaseXXEncodeGetLength(srcBytes, BASE32_ENC_INPUT, BASE32_ENC_OUTPUT);
}

static size_t Base32EncodeA(char* dest, const void* src, size_t srcBytes)
{
    if (dest && src)
    {
        unsigned char* pSrc = (unsigned char*)src;
        char* pDest = dest;
        size_t dwSrcSize = srcBytes;
        size_t dwDestSize = 0;
        size_t dwBlockSize;
        unsigned char n1, n2, n3, n4, n5, n6, n7, n8;

        while (dwSrcSize >= 1)
        {
            /* Encode inputs */
            dwBlockSize = (dwSrcSize < BASE32_ENC_INPUT ? dwSrcSize : BASE32_ENC_INPUT);
            n1 = n2 = n3 = n4 = n5 = n6 = n7 = n8 = 0;
            switch (dwBlockSize)
            {
            case 5:
                n8 = (pSrc[4] & 0x1f);
                n7 = ((pSrc[4] & 0xe0) >> 5);
            case 4:
                n7 |= ((pSrc[3] & 0x03) << 3);
                n6 = ((pSrc[3] & 0x7c) >> 2);
                n5 = ((pSrc[3] & 0x80) >> 7);
            case 3:
                n5 |= ((pSrc[2] & 0x0f) << 1);
                n4 = ((pSrc[2] & 0xf0) >> 4);
            case 2:
                n4 |= ((pSrc[1] & 0x01) << 4);
                n3 = ((pSrc[1] & 0x3e) >> 1);
                n2 = ((pSrc[1] & 0xc0) >> 6);
            case 1:
                n2 |= ((pSrc[0] & 0x07) << 2);
                n1 = ((pSrc[0] & 0xf8) >> 3);
                break;

            default:
                assert(0);
            }
            pSrc += dwBlockSize;
            dwSrcSize -= dwBlockSize;

            /* Validate */
            assert(n1 <= 31);
            assert(n2 <= 31);
            assert(n3 <= 31);
            assert(n4 <= 31);
            assert(n5 <= 31);
            assert(n6 <= 31);
            assert(n7 <= 31);
            assert(n8 <= 31);

            /* Padding */
            switch (dwBlockSize)
            {
            case 1: n3 = n4 = 32;
            case 2: n5 = 32;
            case 3: n6 = n7 = 32;
            case 4: n8 = 32;
            case 5:
                break;

            default:
                assert(0);
            }

            /* 8 outputs */
            *pDest++ = BASE32_ENC_TABLE[n1];
            *pDest++ = BASE32_ENC_TABLE[n2];
            *pDest++ = BASE32_ENC_TABLE[n3];
            *pDest++ = BASE32_ENC_TABLE[n4];
            *pDest++ = BASE32_ENC_TABLE[n5];
            *pDest++ = BASE32_ENC_TABLE[n6];
            *pDest++ = BASE32_ENC_TABLE[n7];
            *pDest++ = BASE32_ENC_TABLE[n8];
            dwDestSize += BASE32_ENC_OUTPUT;
        }
        *pDest++ = '\x0'; /*append terminator*/

        return dwDestSize;
    }
    else
        return 0; /*ERROR - null pointer*/
}

static size_t Base64EncodeGetLength(size_t srcBytes)
{
    return BaseXXEncodeGetLength(srcBytes, BASE64_ENC_INPUT, BASE64_ENC_OUTPUT);
}

static size_t Base64EncodeA(char* dest, const void* src, size_t srcBytes)
{
    if (dest && src)
    {
        unsigned char* pSrc = (unsigned char*)src;
        char* pDest = dest;
        size_t dwSrcSize = srcBytes;
        size_t dwDestSize = 0;
        size_t dwBlockSize = 0;
        unsigned char n1, n2, n3, n4;

        while (dwSrcSize >= 1)
        {
            /* Encode inputs */
            dwBlockSize = (dwSrcSize < BASE64_ENC_INPUT ? dwSrcSize : BASE64_ENC_INPUT);
            n1 = n2 = n3 = n4 = 0;
            switch (dwBlockSize)
            {
            case 3:
                n4 = (pSrc[2] & 0x3f);
                n3 = ((pSrc[2] & 0xc0) >> 6);
            case 2:
                n3 |= ((pSrc[1] & 0x0f) << 2);
                n2 = ((pSrc[1] & 0xf0) >> 4);
            case 1:
                n2 |= ((pSrc[0] & 0x03) << 4);
                n1 = ((pSrc[0] & 0xfc) >> 2);
                break;

            default:
                assert(0);
            }
            pSrc += dwBlockSize;
            dwSrcSize -= dwBlockSize;

            /* Validate */
            assert(n1 <= 63);
            assert(n2 <= 63);
            assert(n3 <= 63);
            assert(n4 <= 63);

            /* Padding */
            switch (dwBlockSize)
            {
            case 1: n3 = 64;
            case 2: n4 = 64;
            case 3:
                break;

            default:
                assert(0);
            }

            /* 4 outputs */
            *pDest++ = BASE64_ENC_TABLE[n1];
            *pDest++ = BASE64_ENC_TABLE[n2];
            *pDest++ = BASE64_ENC_TABLE[n3];
            *pDest++ = BASE64_ENC_TABLE[n4];
            dwDestSize += BASE64_ENC_OUTPUT;
        }
        *pDest++ = '\x0'; /*append terminator*/

        return dwDestSize;
    }
    else
        return 0; /*ERROR - null pointer*/
}

static size_t Base85EncodeGetLength(size_t srcBytes)
{
    return BaseXXEncodeGetLength(srcBytes, BASE85_ENC_INPUT, BASE85_ENC_OUTPUT);
}

static size_t Base85EncodeA(char* dest, const void* src, size_t srcBytes)
{
    if (dest && src)
    {
        unsigned char* pSrc = (unsigned char*)src;
        char* pDest = dest;
        size_t dwSrcSize = srcBytes;
        size_t dwDestSize = 0;
        unsigned int n;
        unsigned char n1, n2, n3, n4, n5;
        size_t i;
        int padding;

        while (dwSrcSize >= 1)
        {
            /* Encode inputs */
            n = 0;
            padding = 0;
            for (i = 0; i < BASE85_ENC_INPUT; ++i)
            {
                n <<= 8;
                if (dwSrcSize >= 1)
                {
                    n |= *pSrc++;
                    --dwSrcSize;
                }
                else
                    ++padding;
            }
#if FOLD_ZERO
            if (n == 0)
            {
                *pDest++ = 'z';
                dwDestSize += 1;
                continue;
            }
#endif

            n5 = (unsigned char)(n % 85);
            n = (n - n5) / 85;
            n4 = (unsigned char)(n % 85);
            n = (n - n4) / 85;
            n3 = (unsigned char)(n % 85);
            n = (n - n3) / 85;
            n2 = (unsigned char)(n % 85);
            n = (n - n2) / 85;
            n1 = (unsigned char)n;

            /* Validate */
            assert(n1 < 85);
            assert(n2 < 85);
            assert(n3 < 85);
            assert(n4 < 85);
            assert(n5 < 85);

            /* Outputs */
            if (padding == 0)
            {
                /* 5 outputs */
                *pDest++ = (n1 + 33);
                *pDest++ = (n2 + 33);
                *pDest++ = (n3 + 33);
                *pDest++ = (n4 + 33);
                *pDest++ = (n5 + 33);
                dwDestSize += BASE85_ENC_OUTPUT;
            }
            else
            {
                /* 1-4 outputs */
                assert(1 <= padding && padding <= 4);
                *pDest++ = (n1 + 33);
                if (padding < 4)
                    *pDest++ = (n2 + 33);
                if (padding < 3)
                    *pDest++ = (n3 + 33);
                if (padding < 2)
                    *pDest++ = (n4 + 33);
                if (padding < 1)
                    *pDest++ = (n5 + 33);
                dwDestSize += (BASE85_ENC_OUTPUT - padding);
            }
        }
        *pDest++ = '\x0'; /*append terminator*/

        return dwDestSize;
    }
    else
        return 0; /*ERROR - null pointer*/
}

/* python functions. */

static PyObject * _base64_b16encode(PyObject *self, PyObject *args) {
  PyObject *bytes_obj;
  if (!PyArg_ParseTuple(args, "O", &bytes_obj))
    return NULL;
  if (PyBytes_Check(bytes_obj))
  {
    Py_ssize_t srclen = PyBytes_GET_SIZE(bytes_obj);
    char * src = PyBytes_AS_STRING(bytes_obj);
    /* the new string that ends up becomming an new 'bytes' like object. */
    size_t required = Base16EncodeGetLength(srclen);
    char *dest = (char*)PyMem_RawMalloc(required);
    ssize_t destlen = Base16EncodeA(dest, src, srclen);
    PyObject * ret = PyBytes_FromStringAndSize(dest, destlen);
    PyMem_RawFree(dest);
    return ret;
  }
  else
  {
    PyErr_SetString(PyExc_TypeError, "a bytes-like object is required.");
  }
  Py_RETURN_NONE;
}

static PyObject * _base64_b16decode(PyObject *self, PyObject *args) {
  PyObject *bytes_obj;
  if (!PyArg_ParseTuple(args, "O", &bytes_obj))
    return NULL;
  if (PyBytes_Check(bytes_obj))
  {
    Py_ssize_t srclen = PyBytes_GET_SIZE(bytes_obj);
    char * src = PyBytes_AS_STRING(bytes_obj);
    /* the new string that ends up becomming an new 'bytes' like object. */
    size_t required = Base16DecodeGetLength(srclen);
    char *dest = (char*)PyMem_RawMalloc(required);
    ssize_t destlen = Base16DecodeA(dest, src, srclen);
    PyObject * ret = PyBytes_FromStringAndSize(dest, destlen);
    PyMem_RawFree(dest);
    return ret;
  }
  else
  {
    PyErr_SetString(PyExc_TypeError, "a bytes-like object is required.");
  }
  Py_RETURN_NONE;
}

static PyObject * _base64_b32encode(PyObject *self, PyObject *args) {
  PyObject *bytes_obj;
  if (!PyArg_ParseTuple(args, "O", &bytes_obj))
    return NULL;
  if (PyBytes_Check(bytes_obj))
  {
    Py_ssize_t srclen = PyBytes_GET_SIZE(bytes_obj);
    char * src = PyBytes_AS_STRING(bytes_obj);
    /* the new string that ends up becomming an new 'bytes' like object. */
    size_t required = Base32EncodeGetLength(srclen);
    char *dest = (char*)PyMem_RawMalloc(required);
    ssize_t destlen = Base32EncodeA(dest, src, srclen);
    PyObject * ret = PyBytes_FromStringAndSize(dest, destlen);
    PyMem_RawFree(dest);
    return ret;
  }
  else
  {
    PyErr_SetString(PyExc_TypeError, "a bytes-like object is required.");
  }
  Py_RETURN_NONE;
}

static PyObject * _base64_b32decode(PyObject *self, PyObject *args) {
  PyObject *bytes_obj;
  if (!PyArg_ParseTuple(args, "O", &bytes_obj))
    return NULL;
  if (PyBytes_Check(bytes_obj))
  {
    Py_ssize_t srclen = PyBytes_GET_SIZE(bytes_obj);
    char * src = PyBytes_AS_STRING(bytes_obj);
    /* the new string that ends up becomming an new 'bytes' like object. */
    size_t required = Base32DecodeGetLength(srclen);
    char *dest = (char*)PyMem_RawMalloc(required);
    ssize_t destlen = Base32DecodeA(dest, src, srclen);
    PyObject * ret = PyBytes_FromStringAndSize(dest, destlen);
    PyMem_RawFree(dest);
    return ret;
  }
  else
  {
    PyErr_SetString(PyExc_TypeError, "a bytes-like object is required.");
  }
  Py_RETURN_NONE;
}

static PyObject * _base64_b64encode(PyObject *self, PyObject *args) {
  PyObject *bytes_obj;
  if (!PyArg_ParseTuple(args, "O", &bytes_obj))
    return NULL;
  if (PyBytes_Check(bytes_obj))
  {
    Py_ssize_t srclen = PyBytes_GET_SIZE(bytes_obj);
    char * src = PyBytes_AS_STRING(bytes_obj);
    /* the new string that ends up becomming an new 'bytes' like object. */
    size_t required = Base64EncodeGetLength(srclen);
    char *dest = (char*)PyMem_RawMalloc(required);
    ssize_t destlen = Base64EncodeA(dest, src, srclen);
    PyObject * ret = PyBytes_FromStringAndSize(dest, destlen);
    PyMem_RawFree(dest);
    return ret;
  }
  else
  {
    PyErr_SetString(PyExc_TypeError, "a bytes-like object is required.");
  }
  Py_RETURN_NONE;
}

static PyObject * _base64_b64decode(PyObject *self, PyObject *args) {
  PyObject *bytes_obj;
  if (!PyArg_ParseTuple(args, "O", &bytes_obj))
    return NULL;
  if (PyBytes_Check(bytes_obj))
  {
    Py_ssize_t srclen = PyBytes_GET_SIZE(bytes_obj);
    char * src = PyBytes_AS_STRING(bytes_obj);
    /* the new string that ends up becomming an new 'bytes' like object. */
    size_t required = Base64DecodeGetLength(srclen);
    char *dest = (char*)PyMem_RawMalloc(required);
    ssize_t destlen = Base64DecodeA(dest, src, srclen);
    PyObject * ret = PyBytes_FromStringAndSize(dest, destlen);
    PyMem_RawFree(dest);
    return ret;
  }
  else
  {
    PyErr_SetString(PyExc_TypeError, "a bytes-like object is required.");
  }
  Py_RETURN_NONE;
}

static PyObject * _base64_b85encode(PyObject *self, PyObject *args) {
  PyObject *bytes_obj;
  if (!PyArg_ParseTuple(args, "O", &bytes_obj))
    return NULL;
  if (PyBytes_Check(bytes_obj))
  {
    Py_ssize_t srclen = PyBytes_GET_SIZE(bytes_obj);
    char * src = PyBytes_AS_STRING(bytes_obj);
    /* the new string that ends up becomming an new 'bytes' like object. */
    size_t required = Base85EncodeGetLength(srclen);
    char *dest = (char*)PyMem_RawMalloc(required);
    ssize_t destlen = Base85EncodeA(dest, src, srclen);
    PyObject * ret = PyBytes_FromStringAndSize(dest, destlen);
    PyMem_RawFree(dest);
    return ret;
  }
  else
  {
    PyErr_SetString(PyExc_TypeError, "a bytes-like object is required.");
  }
  Py_RETURN_NONE;
}

static PyObject * _base64_b85decode(PyObject *self, PyObject *args) {
  PyObject *bytes_obj;
  if (!PyArg_ParseTuple(args, "O", &bytes_obj))
    return NULL;
  if (PyBytes_Check(bytes_obj))
  {
    Py_ssize_t srclen = PyBytes_GET_SIZE(bytes_obj);
    char * src = PyBytes_AS_STRING(bytes_obj);
    /* the new string that ends up becomming an new 'bytes' like object. */
    size_t required = Base85DecodeGetLength(srclen);
    char *dest = (char*)PyMem_RawMalloc(required);
    ssize_t destlen = Base85DecodeA(dest, src, srclen);
    PyObject * ret = PyBytes_FromStringAndSize(dest, destlen);
    PyMem_RawFree(dest);
    return ret;
  }
  else
  {
    PyErr_SetString(PyExc_TypeError, "a bytes-like object is required.");
  }
  Py_RETURN_NONE;
}

static PyMethodDef _base64_methods[] = {
  {"b16encode", (PyCFunction)_base64_b16encode, METH_VARARGS,
   NULL},
  {"b16decode", (PyCFunction)_base64_b16decode, METH_VARARGS,
   NULL},
  {"b32encode", (PyCFunction)_base64_b32encode, METH_VARARGS,
   NULL},
  {"b32decode", (PyCFunction)_base64_b32decode, METH_VARARGS,
   NULL},
  {"b64encode", (PyCFunction)_base64_b64encode, METH_VARARGS,
   NULL},
  {"b64decode", (PyCFunction)_base64_b64decode, METH_VARARGS,
   NULL},
  {"b85encode", (PyCFunction)_base64_b85encode, METH_VARARGS,
   NULL},
  {"b85decode", (PyCFunction)_base64_b85decode, METH_VARARGS,
   NULL},
  {NULL, NULL, 0, NULL}
};

static struct PyModuleDef _base64module = {
  PyModuleDef_HEAD_INIT, "_base64",
  NULL, -1,
  _base64_methods
};

/* declarations for DLL import/export (if used in an embedded */
/* Python interpreter it is necessary to suppress the export  */
/* of the module initialisation function)                     */
#if defined(SUPPRESS_INITFUNC_EXPORT)
#  undef PyMODINIT_FUNC
#  if PY_MAJOR_VERSION >= 3
#    define PyMODINIT_FUNC PyObject *
#  else
#    define PyMODINIT_FUNC void
#  endif
#elif !defined(PyMODINIT_FUNC)
#define PyMODINIT_FUNC void
#endif

PyMODINIT_FUNC
PyInit__base64(void) {
  return PyModule_Create(&_base64module);
}
