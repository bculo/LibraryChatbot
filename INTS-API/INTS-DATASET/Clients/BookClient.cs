using INTS_DATASET.Interfaces;
using INTS_DATASET.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace INTS_DATASET.Clients
{
    public class BookClient : IBookClient
    {
        private readonly string _url = "https://localhost:44368/api/book/filldata";
        private readonly HttpClient _client = new HttpClient();

        public async Task AddBooks(List<BookCSV> books)
        {
            foreach (var book in books)
            {
                var content = JsonConvert.SerializeObject(book);
                var httpResponse = await _client.PostAsync(_url, new StringContent(content, Encoding.Default, "application/json"));
            }
        }
    }
}
