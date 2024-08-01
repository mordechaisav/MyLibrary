namespace MyLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int? BookSetId { get; set; }
        public BookSet BookSet { get; set; }
        public int? ShelfId { get; set; }
        public Shelf Shelf { get; set; }
        public int? LibraryId { get; set; }
        public Library Library { get; set; }
    }
}