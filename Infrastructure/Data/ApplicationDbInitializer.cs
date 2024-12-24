using Core.Entities;
using Core.Entities.Identity;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();

            //Users
            if (!context.Users.Any())
            {
                var users = new ApplicationUser[]
                {
                    new ApplicationUser
                    {
                        UserName = "admin@test.com",
                        Email = "admin@test.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "customer@test.com",
                        Email = "customer@test.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "artistmanager@test.com",
                        Email = "artistmanager@test.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "venuemanager@test.com",
                        Email = "venuemanager@test.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "customer2@test.com",
                        Email = "customer2@test.com",
                    },
                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Password11!");
                }
                await userManager.AddToRoleAsync(users[0], "Admin");
                await userManager.AddToRoleAsync(users[1], "Customer");
                await userManager.AddToRoleAsync(users[2], "ArtistManager");
                await userManager.AddToRoleAsync(users[3], "VenueManager");
                await userManager.AddToRoleAsync(users[4], "Customer");
            }
            //Artists
            if (!context.Artists.Any())
            {
                var artists = new Artist[]
                {
                    new Artist
                    {
                        UserId = 3,
                        Name = "The Testys",
                        About = "We are a Rock Band!",
                        ImageUrl = ""
                    },
                };
                context.Artists.AddRange(artists);
                await context.SaveChangesAsync();
            }
            //Venue
            if (!context.Listings.Any())
            {
                var venue = new Venue
                {
                    UserId = 4,
                    Name = "The Test Venue",
                    About = "Test Venue",
                    Longitude = 0,
                    Latitude = 0,
                    ImageUrl = "",
                    County = "Surrey",
                    Town = "Woking",
                    Approved = true
                };
                context.Venues.Add(venue);
                await context.SaveChangesAsync();
            }
            //Listings
            if (!context.Listings.Any())
            {
                var listings = new Listing[]
                {
                    new Listing
                    {
                        VenueId = 1,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        Pay = 100

                    },
                    new Listing
                    {
                        VenueId = 1,
                        StartDate = new DateTime(2024, 1, 12, 20, 30, 0),
                        EndDate = new DateTime(2024, 1, 12, 23, 30, 0),
                        Pay = 300
                    },
                    new Listing
                    {
                        VenueId = 1,
                        StartDate = new DateTime(2024, 1, 23, 18, 00, 0),
                        EndDate = new DateTime(2024, 1, 23, 20, 00, 0),
                        Pay = 250
                    },
                };
                context.Listings.AddRange(listings);
                await context.SaveChangesAsync();
            }
            //Events
            if (!context.Events.Any())
            {
                var events = new Event[]
                {
                    new Event
                    {
                        ListingId = 2,
                        ArtistId = 1,
                        Price = 10.5,
                        Name = "Test Event",
                        About = "A Test Event",
                        ImageUrl = ""
                    },
                };
                context.Events.AddRange(events);
                await context.SaveChangesAsync();
            }
            //Tickets
            if (!context.Tickets.Any())
            {
                var tickets = new Ticket[]
                 {
                    new Ticket
                    {
                        UserId = 2,
                        EventId = 1,
                        PurchaseDate = DateTime.Now

                    },
                    new Ticket
                    {
                        UserId = 5,
                        EventId = 1,
                        PurchaseDate = DateTime.Now
                    },
                 };
                context.Tickets.AddRange(tickets);
                await context.SaveChangesAsync();
            }
            //Reviews
            if (!context.Users.Any())
            {
                var reviews = new Review[]
                {
                    new Review
                    {
                        TicketId = 1,
                        Stars = 4,
                        Details = "Test"
                    },
                };
                context.Reviews.AddRange(reviews);
                await context.SaveChangesAsync();
            }
        }
    }
}
