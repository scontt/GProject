using GProject.Domain.Entities.Database;
using Microsoft.EntityFrameworkCore;

namespace GProject.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<GamesList> GamesLists { get; set; } = null!;

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
            modelBuilder.Entity<Genre>().HasKey(g => g.Id);
            modelBuilder.Entity<Game>().HasKey(g => g.Id);
            modelBuilder.Entity<GamesList>().HasKey(g => g.Id);

            modelBuilder.Entity<Game>()
                .HasOne(x => x.GameGenre)
                .WithMany(x => x.Games)
                .HasForeignKey(g => g.GenreId);

            modelBuilder.Entity<Game>()
                .HasMany(x => x.Lists)
                .WithMany(x => x.Games);

            modelBuilder.Entity<GamesList>()
                .HasOne(x => x.User)
                .WithMany(x => x.GameLists)
                .HasForeignKey(x => x.CreatorId);
        }
    }
}
