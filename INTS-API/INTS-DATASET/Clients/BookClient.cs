using INTS_DATASET.Interfaces;
using INTS_DATASET.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace INTS_DATASET.Clients
{
    public class BookClient : IBookClient
    {
        private readonly string _url = "https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient _client = new HttpClient();

        public async Task AddBooks(List<BookCSV> task)
        {
            var content = JsonConvert.SerializeObject(task);
            var httpResponse = await _client.PostAsync(_url, new StringContent(content, Encoding.UTF8, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot add books");
            }
        }
    }
}
