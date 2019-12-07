using System.Collections.Generic;

namespace INTS_API.Entities
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
