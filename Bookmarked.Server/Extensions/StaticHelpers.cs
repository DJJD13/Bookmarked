using System.Globalization;

namespace Bookmarked.Server.Extensions;

public static class StaticHelpers
{
    public static DateTime ParseDatePublished(string? dateString)
    {
        if (string.IsNullOrWhiteSpace(dateString))
        {
            return DateTime.MinValue;
        }

        string[] allowedFormats = { "yyyy", "yyyy-mm", "yyyy-mm-dd" };

        return DateTime.TryParseExact(dateString, allowedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None,
            out var datePublished) ? datePublished : DateTime.MinValue;
    }
}