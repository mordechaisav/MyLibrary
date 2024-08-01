namespace MyLibrary.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string Genre { get; set; }
        public List<Shelf> shelves { get; set; } = new List<Shelf>();
    }
}
