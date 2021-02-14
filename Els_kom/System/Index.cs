// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

#if NET472
namespace System
{
    using System.Runtime.CompilerServices;

    internal readonly struct Index : IEquatable<Index>
    {
        private readonly int value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Index(int value, bool fromEnd = false)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative");
            }

            this.value = fromEnd ? ~value : value;
        }

        private Index(int value)
            => this.value = value;

        public static Index Start => new(0);

        public static Index End => new(~0);

        public int Value => this.value < 0 ? ~this.value : this.value;

        public bool IsFromEnd => this.value < 0;

        public static implicit operator Index(int value)
            => FromStart(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Index FromStart(int value)
            => value < 0
            ? throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative")
            : new Index(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Index FromEnd(int value)
            => value < 0
            ? throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative")
            : new Index(~value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetOffset(int length)
        {
            var offset = this.value;
            if (this.IsFromEnd)
            {
                offset += length + 1;
            }

            return offset;
        }

        public override bool Equals(object obj)
            => obj is Index index && this.value == index.value;

        public bool Equals(Index other)
            => this.value == other.value;

        public override int GetHashCode()
            => this.value;

        public override string ToString()
            => this.IsFromEnd ? "^" + ((uint)this.Value).ToString() : ((uint)this.Value).ToString();
    }
}
#endif
