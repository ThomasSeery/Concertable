using Concertible.Entities;
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
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventGenre> EventGenres { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelBooking> HotelBookings { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<TaxiBooking> TaxiBookings { get; set; }
        public DbSet<TaxiCompany> TaxiCompanies { get; set; }
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
                .WithMany(e => e.Venues)  // Establish the one to many relationship between user and venue
                .HasForeignKey(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne<Customer>()
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
                .HasOne<HotelManager>()
                .WithMany(e => e.Hotels)
                .HasForeignKey(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaxiCompany>()
                .HasOne<TaxiManager>()
                .WithMany(e => e.TaxiComapnies)
                .HasForeignKey(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Artist>()
               .HasOne<ArtistManager>()
               .WithMany(e => e.Artists)
               .HasForeignKey(e => e.ApplicationUserId)
               .OnDelete(DeleteBehavior.Cascade);

            //Ensure SeatId and EventId is a unique pair
            modelBuilder.Entity<Ticket>()
                .HasIndex(p => new { p.SeatId, p.EventId }).IsUnique();

            //Specify Keys for ambiguous tables
            modelBuilder.Entity<HotelBooking>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.HotelBooking)
                .HasForeignKey(e => e.HotelBookingId);

            modelBuilder.Entity<TaxiBooking>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.TaxiBooking)
                .HasForeignKey(e => e.TaxiBookingId);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
