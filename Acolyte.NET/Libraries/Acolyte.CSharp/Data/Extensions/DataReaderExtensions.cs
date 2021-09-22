using System;
using System.Data;
using Acolyte.Assertions;
using Acolyte.Common.Monads;

namespace Acolyte.Data
{
    /// <summary>
    /// Contains extension methods for <see cref="IDataReader" /> class.
    /// </summary>
    public static class DataReaderExtensions
    {
        #region Get Classes

        public static T GetClass<T>(this IDataReader dataReader, Func<IDataReader, T> reader)
        {
            dataReader.ThrowIfNull(nameof(dataReader));
            reader.ThrowIfNull(nameof(reader));

            return reader(dataReader);
        }

        public static T? GetClass<T>(this IDataReader dataReader, string name)
            where T : class?
        {
            dataReader.ThrowIfNull(nameof(dataReader));
            name.ThrowIfNull(nameof(name));

            object value = dataReader[name];

            if (value == Convert.DBNull) return null;

            return value.To<T?>();
        }

        public static T? GetClass<T>(this IDataReader dataReader, int index)
            where T : class?
        {
            dataReader.ThrowIfNull(nameof(dataReader));

            object value = dataReader[index];

            if (value == Convert.DBNull) return null;

            return value.To<T?>();
        }

        #endregion

        #region Get Nullable Value Types

        public static T? GetNullable<T>(this IDataReader dataReader, string name)
            where T : struct
        {
            return dataReader.GetNullable<T>(name, defaultValue: null);
        }

        public static T? GetNullable<T>(this IDataReader dataReader, string name, T? defaultValue)
            where T : struct
        {
            dataReader.ThrowIfNull(nameof(dataReader));
            name.ThrowIfNull(nameof(name));

            object value = dataReader[name];

            if (value == Convert.DBNull) return defaultValue;

            return value.To<T?>();
        }

        public static T GetNullable<T>(this IDataReader dataReader, string name, T defaultValue)
            where T : struct
        {
            dataReader.ThrowIfNull(nameof(dataReader));
            name.ThrowIfNull(nameof(name));

            object value = dataReader[name];

            if (value == Convert.DBNull) return defaultValue;

            return value.To<T>();
        }

        #endregion

        #region Get Value Types

        public static T GetValue<T>(this IDataReader dataReader, string name)
            where T : struct
        {
            dataReader.ThrowIfNull(nameof(dataReader));
            name.ThrowIfNull(nameof(name));

            object value = dataReader[name];
            if (value == Convert.DBNull)
            {
                throw new ArgumentException($"Unexpected DBNull value. Name: '{name}'.");
            }

            return value.To<T>();
        }

        public static T GetValue<T>(this IDataReader dataReader, string name, T defaultValue)
            where T : struct
        {
            dataReader.ThrowIfNull(nameof(dataReader));
            name.ThrowIfNull(nameof(name));

            object value = dataReader[name];

            if (value == Convert.DBNull) return defaultValue;

            return value.To<T>();
        }

        #endregion

        #region Get Date Time

        public static DateTime GetDateTime(this IDataReader dataReader, string name,
            DateTimeKind kind)
        {
            dataReader.ThrowIfNull(nameof(dataReader));
            name.ThrowIfNull(nameof(name));

            return DateTime.SpecifyKind(GetValue<DateTime>(dataReader, name), kind);
        }

        #endregion
    }
}
