using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().HasKey(session => new { session.MovieId, session.CinemaId });

            modelBuilder.Entity<Session>().HasOne(session => session.Cinema)
                                          .WithMany(cinema => cinema.Sessions)
                                          .HasForeignKey(session => session.CinemaId);

            modelBuilder.Entity<Session>().HasOne(session => session.Movie)
                                          .WithMany(movie => movie.Sessions)
                                          .HasForeignKey(session => session.MovieId);

            modelBuilder.Entity<Address>().HasOne(address => address.Cinema)
                                          .WithOne(cinema => cinema.Address)
                                          .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}
