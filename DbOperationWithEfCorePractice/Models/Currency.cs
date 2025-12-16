namespace DbOperationWithEfCorePractice.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyDescription { get; set; }
        public string Country { get; set; }

        public ICollection<BookPrice> BookPrices { get; set; }
    }
}
