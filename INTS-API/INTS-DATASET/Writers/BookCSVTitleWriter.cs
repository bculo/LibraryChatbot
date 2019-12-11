using INTS_DATASET.Model;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace INTS_DATASET.Writers
{
    public class BookCSVTitleWriter
    {
        public void WriteToCSVFile(List<BookCSV> titles, string pathOfNewFile)
        {
            var csv = new StringBuilder();

            foreach (var title in titles) {
                var newLine = string.Format("{0};{1}", title.Title, title.LanguageCode);
                csv.AppendLine(newLine);
            }

            File.WriteAllText(pathOfNewFile, csv.ToString());
        }
    }
}
