using System.Collections.Generic;

namespace MyLibrary.Models
{
    public class BookSetViewModel
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
