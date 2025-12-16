namespace DbOperationWithEfCorePractice.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime CreatedOn { get; set; } 
        public int NoOfPages { get; set; } 
        public bool IsActive { get; set; }
        public int LanguageId { get; set; }

        public Language Language { get; set; }

    }
}
