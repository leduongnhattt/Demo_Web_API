using Microsoft.AspNetCore.Identity;

namespace Lesson1API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
