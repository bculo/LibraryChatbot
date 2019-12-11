using INTS_DATASET.Interfaces;
using INTS_DATASET.Model;
using INTS_DATASET.Utils;
using INTS_DATASET.Writers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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
            List<BookCSV> booksForCsv = new List<BookCSV>();

            //api komunikacija
            foreach (var book in books)
            {
                var content = JsonConvert.SerializeObject(book);
                var httpResponse = await _client.PostAsync(_url, new StringContent(content, Encoding.Default, "application/json"));

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    booksForCsv.Add(book);
            }

            //pripremi CSV putanju
            string projectPath = PathUtils.GetProjectDirectoryPath();
            string csvPath = Path.Combine(projectPath, "Datasets", "titles.csv");

            //upisi u novi CSV samo titlove i kod jezika
            new BookCSVTitleWriter().WriteToCSVFile(booksForCsv, csvPath);
        }
    }
}
