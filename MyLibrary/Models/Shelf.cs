namespace MyLibrary.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }
        public double Width {  get; set; }
        public double Height { get; set; }
        public double Space { get; set; }
        public List<Book> books { get; set; } = new List<Book>();
    }
}