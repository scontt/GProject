using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RollerFate.Domain.Entities.Auth;
using RollerFate.Domain.Entities.Database;

namespace RollerFate.Infrastructure.Persistence.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<GameList> GamesLists { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        private readonly string _connectionString;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration) : base(options)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
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
            modelBuilder.Entity<GameList>().HasKey(g => g.Id);
            modelBuilder.Entity<RefreshToken>().HasKey(r => r.Id);

            modelBuilder.Entity<Game>()
                .HasOne(x => x.GameGenre)
                .WithMany(x => x.Games)
                .HasForeignKey(g => g.GenreId);

            modelBuilder.Entity<Game>()
                .HasMany(x => x.Lists)
                .WithMany(x => x.Games)
                .UsingEntity<GamesGameList>(
                l => l.HasOne<GameList>().WithMany().HasForeignKey(x => x.ListId),
                r => r.HasOne<Game>().WithMany().HasForeignKey(x => x.GameId),
                j =>
                {
                    j.ToTable("GamesGameLists");
                    j.HasKey(nameof(GamesGameList.GameId), nameof(GamesGameList.ListId));
                });

            modelBuilder.Entity<GameList>()
                .HasOne(x => x.User)
                .WithMany(x => x.GameLists)
                .HasForeignKey(x => x.CreatorId);
        }
    }
}
