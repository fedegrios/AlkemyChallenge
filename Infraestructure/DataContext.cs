using Domain;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(AppConfiguration.ConnectionString);

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CharacterMovie>()
                .HasKey(cm => new { cm.CharacterId, cm.MovieId });

            builder.Entity<GenreMovie>()
                .HasKey(gm => new { gm.GenreId, gm.MovieId });
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<GenreMovie> GenreMovies { get; set; }
        public DbSet<CharacterMovie> CharacterMovies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
