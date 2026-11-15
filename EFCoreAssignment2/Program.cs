using EFCoreAssignment2.Configurations;
using EFCoreAssignment2.Contexts;
using Microsoft.EntityFrameworkCore;
using EFCoreAssignment2.Models;

namespace EFCoreAssignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new EventHubDbContext();

            var organizer = new Organizer
            {
                Name = "Soha Ahmed",
                CompanyName = "Nile Events Company",
                IsVerified = true,
                Profile = new OrganizerProfile
                {
                    Biography = "We organize tech conferences",
                    WebsiteUrl = "https://nileevents.com",
                    LogoUrl = "/logos/nile.png"
                },

                Events =
                { 
                    new Event
                    { 
                        Title = "EF Core Workshop",
                        Description = "A full-day hands-on workshop about EF Core.",
                        StartDate = new DateTime(2026, 12, 5, 10, 0, 0),
                        EndDate = new DateTime(2026, 12, 5, 17, 0, 0),
                        MaxAttendees = 100
                    },

                    new Event
                    { 
                        Title = "ASP.NET Meetup",
                        Description = "Monthly community meetup.",
                        StartDate = new DateTime(2026, 12, 20, 18, 0, 0),
                        MaxAttendees = 50
                    }
                }
            };

            dbContext.Organizers.Add(organizer);

            var attendee = new Attendee
            {
                FullName = "Ali Mohammed",
                Email = "Ali@example.com",
                Street = "12 Tahrir St",
                City = "Cairo",
                Country = "Egypt",
                PostalCode = "11511",
                Badge = new Badge
                {
                    BadgeNumber = "BDG-0001",
                    IssuedDate = DateTime.UtcNow,
                    Tier = BadgeTier.VIP
                }
            };

            dbContext.Attendees.Add(attendee);

            dbContext.SaveChanges();

            var result = dbContext.Organizers
                                .Include(o => o.Profile)
                                .Include(o => o.Events)
                                .First();

            Console.WriteLine($" Organizer: {result.Name} ({result.CompanyName}) - Verified: {result.IsVerified}");
            Console.WriteLine($" Profile  : {result.Profile?.WebsiteUrl}");
            foreach (var e in result.Events)
                Console.WriteLine($" Event  : {e.Title} | {e.StartDate:g} -> {(e.EndDate?.ToString("g") ?? "N/A")} | max {e.MaxAttendees}");

            var a = dbContext.Attendees.Include(x => x.Badge).First();
            Console.WriteLine($" Attendee : {a.FullName} <{a.Email}>");
            Console.WriteLine($" Address  : {a.FullAddress}");
            Console.WriteLine($" Badge    : {a.Badge?.BadgeNumber} ({a.Badge?.Tier}) issued {a.Badge?.IssuedDate:d}");

        }
    }
}
