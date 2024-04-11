using System.Security.Claims;

namespace Bookmarked.Server.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.GivenName)).Value;
        }
    }
}
