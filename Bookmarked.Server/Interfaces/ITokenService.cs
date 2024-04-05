using Bookmarked.Server.Models;

namespace Bookmarked.Server.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
