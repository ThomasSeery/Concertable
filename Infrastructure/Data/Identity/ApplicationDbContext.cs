using Core.Entities;
using Core.Entities.Identity;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;

namespace Infrastructure.Data.Identity
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, int>(options)
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistGenre> ArtistGenres { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventGenre> EventGenres { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingGenre> ListingGenres { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<VenueImage> VenueImages { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Add ApplicationUser Forein Keys
            /*
             * Since ApplicationUser is defined in Infrastructure, it cant be
             * referenced in core.
             * so we need to explicitly reference the relationships here
             */
            modelBuilder.Entity<Venue>()
                .HasOne<VenueManager>()
                .WithOne(e => e.Venue)  // Establish the one to one relationship between user and venue
                .HasForeignKey<Venue>(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne<Customer>()
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Artist>()
               .HasOne<ArtistManager>()
               .WithOne(e => e.Artist)
               .HasForeignKey<Artist>(e => e.ApplicationUserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        
        }

    }
}
