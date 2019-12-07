using System;
using System.Collections.Generic;

namespace INTS_API.Entities
{
    public class BookCopy : Entity<Guid>
    {
        public bool Borrowed { get; set; }
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }

        //vanjski kljuc na knjigu
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
