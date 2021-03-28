using System;

namespace Acolyte.Functions
{
    /// <summary>
    /// Helper class that provides set of static functions to inverse values.
    /// </summary>
    public static class InverseFunction
    {
        #region Obsolete

        [Obsolete("Use \"ForIntt32\" instead. This property will be remove in next major version.")]
        public static Func<int, int> Int32 { get; } = x => -x;

        [Obsolete("Use \"ForInt64\" instead. This property will be remove in next major version.")]
        public static Func<long, long> Int64 { get; } = x => -x;

        [Obsolete("Use \"ForSingle\" instead. This property will be remove in next major version.")]
        public static Func<float, float> Single { get; } = x => -x;

        [Obsolete("Use \"ForDouble\" instead. This property will be remove in next major version.")]
        public static Func<double, double> Double { get; } = x => -x;

        [Obsolete("Use \"ForDecimal\" instead. This property will be remove in next major version.")]
        public static Func<decimal, decimal> Decimal { get; } = x => -x;

        #endregion

        /// <summary>
        /// Function to inverse <see cref="sbyte" /> values.
        /// </summary>
        public static Func<sbyte, sbyte> ForSByte { get; } = x => (sbyte) -x;

        /// <summary>
        /// Function to inverse <see cref="short" /> values.
        /// </summary>
        public static Func<short, short> ForInt16 { get; } = x => (short) -x;

        /// <summary>
        /// Function to inverse <see cref="int" /> values.
        /// </summary>
        public static Func<int, int> ForInt32 { get; } = x => -x;

        /// <summary>
        /// Function to inverse <see cref="long" /> values.
        /// </summary>
        public static Func<long, long> ForInt64 { get; } = x => -x;

        /// <summary>
        /// Function to inverse <see cref="float" /> values.
        /// </summary>
        public static Func<float, float> ForSingle { get; } = x => -x;

        /// <summary>
        /// Function to inverse <see cref="double" /> values.
        /// </summary>
        public static Func<double, double> ForDouble { get; } = x => -x;

        /// <summary>
        /// Function to inverse <see cref="decimal" /> values.
        /// </summary>
        public static Func<decimal, decimal> ForDecimal { get; } = x => -x;
    }
}
