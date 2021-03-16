namespace Acolyte.Tests
{
    public static class TestHelper
    {
        // Max collection size in C# is equal to 2_146_435_071 but we do not need such large values.
        public const int NegativeMaxCollectionSize = -MaxCollectionSize;

        public const int NegativeTenThousand = -TenThousand;

        public const int NegativeHundred = -Hundred;

        public const int NegativeTen = -Ten;

        public const int NegativeFive = -Five;

        public const int NegativeTwo = -Two;

        public const int NegativeOne = -One;

        public const int Zero = 0;

        public const int One = 1;

        public const int Two = 2;

        public const int Five = 5;

        public const int Ten = 10;

        public const int Hundred = 100;

        public const int TenThousand = 10_000;

        // Max collection size in C# is equal to 2_146_435_071 but we do not need such large values.
        public const int MaxCollectionSize = 2_146_435;
    }
}
