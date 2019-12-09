using INTS_API.Entities;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IBookService
    {
        Task AddBokk(Book book);
    }
}
