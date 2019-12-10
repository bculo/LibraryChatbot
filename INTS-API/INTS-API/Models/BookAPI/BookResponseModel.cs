using System;

namespace INTS_API.Models.BookAPI
{
    public class BookResponseModel
    {
        public Guid Id;
        public string Title { get; set; }
        public string Authors { get; set; }
        public double AvarageRating { get; set; }
        public string ISBN { get; set; }
        public string ISBN13 { get; set; }
        public string LanguageCode { get; set; }
        public int PageNumber { get; set; }
        public int RatingsCount { get; set; }
        public int CategoryID { get; set; }
    }
}
