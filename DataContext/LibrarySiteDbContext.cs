using LibrarySite.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySite.MVC6.DataContext
{
    public class LibrarySiteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=LibrarySiteDb;User Id=sa;Password=k1234567;");
        }
    }
}
