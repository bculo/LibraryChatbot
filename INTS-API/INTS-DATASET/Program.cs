using INTS_DATASET.Model;
using INTS_DATASET.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace INTS_DATASET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //pripremi CSV path
            string projectPath = GetProjectDirectoryPath();
            string csvPath = Path.Combine(projectPath, "Datasets", "books.csv");

            //procitaj csv datoteku
            List<BookCSV> books = new BookCSVReader().GetIntances(csvPath);

            //posalji podatke na API endpoint
            Task.Run(async () =>
            {
                await LibraryClientFactory.GetBookClient().AddBooks(books);
            });

            Console.ReadLine();
        }

        public static string GetProjectDirectoryPath()
        {
            return AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
        }
    }
}
