using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class BookStoreContext:IdentityDbContext<ApplicationUser>
    {
        public IConfiguration Configuration { get; }
        private readonly string _connectionString;
        public BookStoreContext(DbContextOptions<BookStoreContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("BookStoreDb");
        }

        public DbSet<Books> Books { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Cart>()
        //        .HasRequired(c => c.ApplicationUser)
        //        .WithMany(t => t.MyObjects)
        //        .Map(m => m.MapKey("UserId"));
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=;Database=BookStore;Integrated Security=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
