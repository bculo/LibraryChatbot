using INTS_DATASET.Model;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace INTS_DATASET.Readers
{
    public class BookCSVReader
    {
        public bool ValidFile(string filePath)
        {
            if (File.Exists(filePath))
                return true;
            return false;
        }

        public List<BookCSV> GetIntances(string filePath)
        {
            if (!ValidFile(filePath))
            {
                Console.WriteLine($"File {filePath} doesnt exist");
                return new List<BookCSV>();
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

            //pretvori u listu
            var resultList = result.ToList();

            //makni knjigu the earth
            resultList.RemoveAll(i => i.Title == "The Earth");

            //modifikacija jezicnog koda i makivanje ; iz naslova
            foreach (var book in resultList)
            {
                book.LanguageCode = "en";

                if (book.Title.Contains(";"))
                    book.Title = book.Title.Replace(";", "");
            }

            return resultList;
        }

        private IEnumerable<BookCSV> ReadFile(string filePath)
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

                    BookCSV instance = CreateInstance(rowData);
                    if (instance != null)
                        yield return instance;
                }
            }
        }

        private BookCSV CreateInstance(string[] rowData)
        {
            try
            {
                return new BookCSV()
                {
                    Title = rowData[1],
                    Authors = rowData[2],
                    AvarageRating = double.Parse(rowData[3]),
                    ISBN = rowData[4],
                    ISBN13 = rowData[5],
                    ////LanguageCode = rowData[6],
                    PageNumber = int.Parse(rowData[7]),
                    RatingsCount = int.Parse(rowData[8]),
                    TextReviewsCount = int.Parse(rowData[9])
                };
            }
            catch 
            {
                return null;
            }
        }
    }
}
