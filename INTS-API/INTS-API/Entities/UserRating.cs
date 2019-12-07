using System;

namespace INTS_API.Entities
{
    public class UserRating
    {
        public int Rate { get; set; }

        //vanjski kljuc na user-a
        public Guid UserId { get; set; }
        public User User { get; set; }

        //vanjski kljuc na knjigu
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
