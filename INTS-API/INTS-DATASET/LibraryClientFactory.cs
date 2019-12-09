using INTS_DATASET.Clients;
using INTS_DATASET.Interfaces;

namespace INTS_DATASET
{
    public static class LibraryClientFactory
    {
        public static IBookClient GetBookClient()
        {
            return new BookClient();
        }
    }
}
