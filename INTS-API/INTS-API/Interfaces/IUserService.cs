using INTS_API.Models.ServiceResult;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IUserService
    {
        Task<AuthLoginResult> Login(string userName, string plainPassword);
        Task<AuthRegistrationResult> Register(string userName, string plainPassword);
    }
}
