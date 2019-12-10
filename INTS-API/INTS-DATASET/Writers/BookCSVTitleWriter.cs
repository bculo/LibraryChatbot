using INTS_DATASET.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace INTS_DATASET.Writers
{
    public class BookCSVTitleWriter
    {
        public void WriteToCSVFile(List<BookTitle> titles, string pathOfNewFile)
        {
            var csv = new StringBuilder();

            //Remove duplicates
            titles = titles.GroupBy(i => i.Title).Select(i => i.First()).ToList();

            foreach (var title in titles) { 
                var newLine = string.Format("{0}", title.Title);
                csv.AppendLine(newLine);
            }

            File.WriteAllText(pathOfNewFile, csv.ToString());
        }
    }
}
