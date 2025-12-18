namespace DbOperationWithEfCorePractice.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }

       public ICollection<Book> Books { get; set; }
    }
}
