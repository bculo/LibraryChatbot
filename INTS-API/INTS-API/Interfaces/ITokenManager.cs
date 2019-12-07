using INTS_API.Entities;

namespace INTS_API.Interfaces
{
    public interface ITokenManager
    {
        string CreateJWTToken(User user);
    }
}
