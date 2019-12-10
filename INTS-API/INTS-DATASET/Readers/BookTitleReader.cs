using INTS_DATASET.Model;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace INTS_DATASET.Readers
{
    public class BookTitleReader
    {
        public bool ValidFile(string filePath)
        {
            if (File.Exists(filePath))
                return true;
            return false;
        }

        public List<BookTitle> GetIntances(string filePath)
        {
            if (!ValidFile(filePath))
            {
                Console.WriteLine($"File {filePath} doesnt exist");
                return new List<BookTitle>();
            }

            //procitaj csv
            var result = ReadFile(filePath);

            //makni duplikate
            result = result.GroupBy(i => i.Title).Select(i => i.First());

            //prebroji element niza
            int count = result.Count();

            //uzmi samo 9000 zapisa
            if (count > 9000)
                result = result.Take(9000);


            var resultList = result.ToList();

            //modifikacija
            foreach (var book in resultList)
            {
                book.LanguageCode = "en";

                if (book.Title.Contains(";"))
                    book.Title = book.Title.Replace(';', ' ');
            }

            return resultList;
        }

        private IEnumerable<BookTitle> ReadFile(string filePath)
        {
            using (TextFieldParser csvReader = new TextFieldParser(filePath))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                //procitaj header
                string[] colFields = csvReader.ReadFields();

                //citaj redove sa podacima
                while (!csvReader.EndOfData)
                {
                    string[] rowData;
                    try
                    {
                        rowData = csvReader.ReadFields();
                    }
                    catch
                    {
                        continue;
                    }

                    BookTitle instance = CreateInstance(rowData);
                    if (instance != null)
                        yield return instance;
                }
            }
        }

        private BookTitle CreateInstance(string[] rowData)
        {
            try
            {
                return new BookTitle()
                {
                    Title = rowData[1],
                    //LanguageCode = rowData[6],
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
