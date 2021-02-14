namespace Acolyte.Common
{
    internal static class Strings
    {
        public static string MoreThanOneElementErrorMessage { get; } =
            "Sequence contains more than one element.";

        public static string MoreThanOneMatchErrorMessage { get; } =
            "Sequence contains more than one matching element.";

        public static string NoElementsErrorMessage { get; } =
            "Sequence contains no elements.";

        public static string NoMatchErrorMessage { get; } =
            "Sequence contains no matching element.";

        public static string DefaultEmptyCollectionMessage { get; } = "None";

        public static string DefaultItemSeparator { get; } = ", ";
    }
}
