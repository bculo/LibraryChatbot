using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IHasher
    {
        Task<string> HashPassword(string plainPassword);
        Task<bool> CheckPassword(string userCredentials, string plainPassword);
    }
}
