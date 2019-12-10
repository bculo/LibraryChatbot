using INTS_DATASET.Model;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace INTS_DATASET.Readers
{
    class BookTitleReader
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

            return ReadFile(filePath).ToList();
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
                    LanguageCode = rowData[6],
                    Title = rowData[1],
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
