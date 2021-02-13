// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

#if NET472
namespace System.Runtime.CompilerServices
{
    internal static class RuntimeHelpers
    {
        public static T[] GetSubArray<T>(T[] array, Range range)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            (var offset, var length) = range.GetOffsetAndLength(array.Length);
            if (default(T) != null || typeof(T[]) == array.GetType())
            {
                // We know the type of the array to be exactly T[].
                if (length == 0)
                {
                    return Array.Empty<T>();
                }

                var dest = new T[length];
                Array.Copy(array, offset, dest, 0, length);
                return dest;
            }
            else
            {
                // The array is actually a U[] where U:T.
                var dest = (T[])Array.CreateInstance(array.GetType().GetElementType(), length);
                Array.Copy(array, offset, dest, 0, length);
                return dest;
            }
        }
    }
}
#endif
