using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        /*
            Handles communication to the database.
        */
        
        public DataContext(DbContextOptions options) : base(options)
        {}

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Item> Items    { get; set; }
    }
}