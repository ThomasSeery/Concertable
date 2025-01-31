﻿using Core.Entities;
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
                .HasOne(e => e.Register)
                .WithOne()
                .HasForeignKey<Event>(e => e.RegisterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Register>()
                .HasOne(r => r.Listing)
                .WithMany(l => l.Registers)  
                .HasForeignKey(r => r.ListingId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction); 

            // One-to-Many: An Artist can have multiple Registers
            modelBuilder.Entity<Register>()
                .HasOne(r => r.Artist)
                .WithMany(a => a.Registers)  
                .HasForeignKey(r => r.ArtistId)
                .IsRequired();

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany(e => e.Tickets) 
                .HasForeignKey(t => t.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany() // ✅ No explicit Tickets collection in User
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        
        }

    }
}
