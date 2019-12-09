using INTS_DATASET.Model;
using INTS_DATASET.Readers;
using System;
using System.Collections.Generic;
using System.IO;

namespace INTS_DATASET
{
    class Program
    {
        static void Main(string[] args)
        {
            //pripremi CSV path
            string projectPath = GetProjectDirectoryPath();
            string csvPath = Path.Combine(projectPath, "Datasets", "books.csv");

            //procitaj csv datoteku
            List<BookCSV> books = new BookCSVReader().GetIntances(csvPath);

            //posalji podatke na API endpoint
            LibraryClientFactory.GetBookClient().AddBooks(books);
        }

        public static string GetProjectDirectoryPath()
        {
            return AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
        }
    }
}
