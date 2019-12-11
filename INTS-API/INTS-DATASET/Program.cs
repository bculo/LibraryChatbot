using INTS_DATASET.Model;
using INTS_DATASET.Readers;
using INTS_DATASET.Utils;
using INTS_DATASET.Writers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace INTS_DATASET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //pripremi CSV putanju
            string projectPath = PathUtils.GetProjectDirectoryPath();
            string csvPath = Path.Combine(projectPath, "Datasets", "books.csv");

            //procitaj App.config
            bool readEverything = bool.Parse(ConfigurationManager.AppSettings["readfullcsv"]);
            if (readEverything)
            {
                //procitaj csv datoteku
                List<BookCSV> bookRecords = new BookCSVReader().GetIntances(csvPath);

                //posalji podatke na API endpoint
                Task.Run(async () =>
                {
                    await LibraryClientFactory.GetBookClient().AddBooks(bookRecords);
                });
            }

            Console.WriteLine("END OF WORK!");
            Console.ReadLine();
        }
    }
}
