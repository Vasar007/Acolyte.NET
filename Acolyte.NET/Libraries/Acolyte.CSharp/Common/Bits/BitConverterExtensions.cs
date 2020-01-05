namespace Acolyte.Common.Bits
{
    /// <summary>
    /// Contains extension methods to work with bits.
    /// </summary>
    public static class BitConverterExtensions
    {
        // Reverse byte order (16-bit).
        public static ushort ReverseBytes(this ushort value)
        {
            return (ushort) ((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }

        // Reverse byte order (32-bit).
        public static uint ReverseBytes(this uint value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8  | (value & 0xFF000000U) >> 24;
        }

        // Reverse byte order (64-bit).
        public static ulong ReverseBytes(this ulong value)
        {
            return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                   (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8  |
                   (value & 0x000000FF00000000UL) >> 8  | (value & 0x0000FF0000000000UL) >> 24 |
                   (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }

        public static ushort ReverseBits(this ushort value)
        {
            ushort y = 0;
            for (int i = 0; i < 16; ++i)
            {
                y <<= 1;
                y |= (ushort) (value & 1);
                value >>= 1;
            }
            return y;
        }

        public static uint ReverseBits(this uint value)
        {
            uint y = 0;
            for (int i = 0; i < 32; ++i)
            {
                y <<= 1;
                y |= (value & 1);
                value >>= 1;
            }
            return y;
        }

        public static ulong ReverseBits(this ulong value)
        {
            ulong y = 0;
            for (int i = 0; i < 64; ++i)
            {
                y <<= 1;
                y |= value & 1;
                value >>= 1;
            }
            return y;
        }
    }
}
