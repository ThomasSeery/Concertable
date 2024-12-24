using Core.Entities;
using Core.Entities.Identity;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;
using static Core.Entities.Identity.Manager;

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
        public DbSet<Message> Messages { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<VenueImage> VenueImages { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Add ApplicationUser Forein Keys
            /*
             * Where the names between the ids and objects do not coinside
             * e.g. FromId (should be FromUserId to match) and FromUser, we need to
             * explicitly reference these here so .NET knows the relationship between them
             */
            //modelBuilder.Entity<Venue>()
            //    .HasOne<VenueManager>()
            //    .WithOne(e => e.Venue)  // Establish the one to one relationship between user and venue
            //    .HasForeignKey<Venue>(e => e.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Ticket>()
            //    .HasOne<Customer>()
            //    .WithMany(e => e.Tickets)
            //    .HasForeignKey(e => e.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Artist>()
            //   .HasOne<ArtistManager>()
            //   .WithOne(e => e.Artist)
            //   .HasForeignKey<Artist>(e => e.UserId)
            //   .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne<Manager>()
                .WithMany(e => e.SentMessages)
                .HasForeignKey(m => m.FromId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne<Manager>()
                .WithMany(e => e.ReceivedMessages)
                .HasForeignKey(m => m.ToId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.FromUser)
                .WithMany()
                .HasForeignKey(m => m.FromId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.ToUser)
                .WithMany()
                .HasForeignKey(m => m.ToId)
                .OnDelete(DeleteBehavior.Restrict);

            //Temporary Fix
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Listing)
                .WithMany()
                .HasForeignKey(e => e.ListingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Register>()
                .HasOne(e => e.Listing)
                .WithMany()
                .HasForeignKey(e => e.ListingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(e => e.Event)
                .WithMany()
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        
        }

    }
}
