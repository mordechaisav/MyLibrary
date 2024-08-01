using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibrary.Models;


namespace MyLibrary.ViewModel
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public SelectList Shelves { get; set; }
    }
}
