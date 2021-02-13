// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

#if NET472
namespace System
{
    using System.Runtime.CompilerServices;

    internal readonly struct Range : IEquatable<Range>
    {
        public Range(Index start, Index end)
        {
            this.Start = start;
            this.End = end;
        }

        public static Range All => new(Index.Start, Index.End);

        public Index Start { get; }

        public Index End { get; }

        public static Range StartAt(Index start)
            => new(start, Index.End);

        public static Range EndAt(Index end)
            => new(Index.Start, end);

        public override bool Equals(object obj)
            => obj is Range r &&
            r.Start.Equals(this.Start) &&
            r.End.Equals(this.End);

        public bool Equals(Range other)
            => other.Start.Equals(this.Start) && other.End.Equals(this.End);

        public override int GetHashCode()
            => (this.Start.GetHashCode() * 31) + this.End.GetHashCode();

        public override string ToString()
            => this.Start + ".." + this.End;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int Offset, int Length) GetOffsetAndLength(int length)
        {
            int start;
            var startIndex = this.Start;
            start = startIndex.IsFromEnd ? length - startIndex.Value : startIndex.Value;
            int end;
            var endIndex = this.End;
            end = endIndex.IsFromEnd ? length - endIndex.Value : endIndex.Value;
            return (uint)end > (uint)length || (uint)start > (uint)end
                ? throw new ArgumentOutOfRangeException(nameof(length))
                : (start, end - start);
        }
    }
}
#endif
