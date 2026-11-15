using Microsoft.EntityFrameworkCore;
using EFCoreAssignment2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using EFCoreAssignment2.Configurations;

namespace EFCoreAssignment2.Contexts
{
    internal class EventHubDbContext : DbContext 
    {
        public DbSet<Attendee> Attendees => Set<Attendee>();
        public DbSet<Badge> Badges => Set<Badge>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Organizer> Organizers => Set<Organizer>();
        public DbSet<OrganizerProfile> OrganizerProfiles => Set<OrganizerProfile>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database=EventHub; Trusted_Connection=true; TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrganizerProfileConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());

            modelBuilder.Entity<Badge>(b =>
            {
                b.ToTable("Badges");

                b.HasKey(x => x.Id);

                b.Property(x => x.BadgeNumber)
                 .IsRequired()
                 .HasMaxLength(50);

                b.HasIndex(x => x.BadgeNumber)
                 .IsUnique();

                b.Property(x => x.IssuedDate)
                 .IsRequired()
                 .HasDefaultValueSql("GETUTCDATE()");

                b.Property(x => x.Tier)
                 .IsRequired()
                 .HasConversion<string>()
                 .HasMaxLength(20);

                b.HasOne(a => a.Attendee)
                 .WithOne(x => x.Badge)
                 .HasForeignKey<Badge>(x => x.AttendeeId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);
            }
            );
        }
    }
}
