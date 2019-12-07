using System;
using System.Collections.Generic;

namespace INTS_API.Entities
{
    public class Book : Entity<Guid>
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public double AvarageRating { get; set; }
        public string ISBN { get; set; }
        public string ISBN13 { get; set; }
        public string LanguageCode { get; set; }
        public int PageNumber { get; set; }
        public int RatingsCount { get; set; }
        public ICollection<BookCopy> BookCopies { get; set; }
        public ICollection<UserRating> UserRatings { get; set; }

        //vanjski kljuc na kategorije
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
