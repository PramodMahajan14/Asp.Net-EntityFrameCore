using Microsoft.EntityFrameworkCore;

namespace webapp.data
{
    public class AppDBContext : DbContext
    {
        // Store the configuration in a class-level field
        protected readonly IConfiguration _config;

        // Constructor
        public AppDBContext(DbContextOptions<AppDBContext> options, IConfiguration config) : base(options)
        {
            _config = config;  // Assign the parameter to the class field
        }

        // Configure DbContext to use Npgsql (PostgreSQL)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use the connection string from the configuration
            optionsBuilder.UseNpgsql(_config.GetConnectionString("AppDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
               new Currency() { Id = 1, Title = "INR", Description = "Indian INR" },
               new Currency() { Id = 2, Title = "Dollar", Description = "Dollar" },
               new Currency() { Id = 3, Title = "Euro", Description = "Euro" },
               new Currency() { Id = 4, Title = "Dinar", Description = "Dinar" }
               );

            modelBuilder.Entity<Langauge>().HasData(
               new Currency() { Id = 1, Title = "Marathi", Description = "Marathi" },
               new Currency() { Id = 2, Title = "Hindi", Description = "Hindi" },
               new Currency() { Id = 3, Title = "English", Description = "English" },
               new Currency() { Id = 4, Title = "Punjabi", Description = "Punjabi" }
               );
        }

        // Define DbSets for your entities
        public DbSet<Book> Book { get; set; }
        public DbSet<Langauge> Language { get; set; }  

        public DbSet<Currency> Currency { get; set; }

        public DbSet<BookPrice> BookPrice { get; set; }
        public DbSet<Author> Author { get; set; }
    }
}
