using System.Threading.Tasks;
using INTS_API.Interfaces;
using INTS_API.Models.ServiceResult;

namespace INTS_API.Services
{
    public class UserService : IUserService
    {
        protected readonly IHasher _hasher;
        protected readonly ITokenManager _token;

        public UserService(IHasher hasher, ITokenManager token)
        {
            _hasher = hasher;
            _token = token;
        }

        public virtual async Task<AuthLoginResult> Login(string userName, string plainPassword)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<AuthRegistrationResult> Register(string userName, string plainPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}
