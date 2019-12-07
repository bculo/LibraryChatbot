using System;
using System.Collections.Generic;

namespace INTS_API.Entities
{
    public class User : Entity<Guid>
    {
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public ICollection<UserRating> UserRatings { get; set; }
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
