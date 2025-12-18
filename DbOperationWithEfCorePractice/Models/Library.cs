namespace DbOperationWithEfCorePractice.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string BooksSection { get; set; } = null!;
        public string SectionInfo { get; set; } = null!;
        public string BookType { get; set; } = null!;
        public long NumberOfBooks { get; set; }
        public long NumberOfBooksPerSection { get; set; }
        public long LibrarianId { get; set; }
        public bool IsActive { get; set; }
        public Librarian Librarian { get; set; } = null!;
    }
}
