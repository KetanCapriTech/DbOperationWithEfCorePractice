using DbOperationWithEfCorePractice.Models;

namespace DbOperationWithEfCorePractice.Dtos
{
    public class BookDto
    {
        public string Description { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }
        public int LanguageId { get; set; }
        public int AuthorId { get; set; }

        public Author? Author { get; set; }

    }
}
