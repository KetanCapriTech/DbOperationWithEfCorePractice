using DbOperationWithEfCorePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEfCorePractice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Country = "India", CurrencyDescription = "Indian rupees", CurrencyName = "INR" },
                new Currency { Id = 2, Country = "USA", CurrencyDescription = "American doller", CurrencyName = "DOLLER" },
                new Currency { Id = 3, Country = "European", CurrencyDescription = "European eurp", CurrencyName = "EURO" },
                new Currency { Id = 4, Country = "JAPAN", CurrencyDescription = "Japanees yen", CurrencyName = "YEN" }
            );

            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, LanguageName = "Hindi" },
                new Language { Id = 2, LanguageName = "English" },
                new Language { Id = 3, LanguageName = "English" },
                new Language { Id = 4, LanguageName = "English" }
            );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}
