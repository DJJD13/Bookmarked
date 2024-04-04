namespace Bookmarked.Server.Helpers
{
    public static class StringExtensions
    {
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}
