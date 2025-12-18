namespace DbOperationWithEfCorePractice.Models
{
    public class Librarian
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsFullTime { get; set; } 
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string HighestQualification { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
