using System;

namespace INTS_API.Entities
{
    public class BorrowedBook : Entity<Guid>
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime? ReturnDateTime { get; set; }

        //vanjski kljuc na kopiju knjige
        public Guid BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }

        //vanjski kljuc na user-a
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
