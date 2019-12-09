using INTS_DATASET.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_DATASET.Interfaces
{
    public interface IBookClient
    {
        Task AddBooks(List<BookCSV> books);
    }
}
