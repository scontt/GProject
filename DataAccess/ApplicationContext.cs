using GProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GProject.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        private readonly string _connectionString = "Host=127.0.0.1;Password=hitler7;Persist Security Info=True;Username=postgres;Database=gproject";

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
        }
    }
}
