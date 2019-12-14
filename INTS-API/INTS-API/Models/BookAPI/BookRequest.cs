using System.ComponentModel.DataAnnotations;

namespace INTS_API.Models.BookAPI
{
    public class BookRequest
    {
        public int? Number { get; set; }
        public string Category { get; set; }
    }
}
