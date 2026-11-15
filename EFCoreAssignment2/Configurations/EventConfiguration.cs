using EFCoreAssignment2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreAssignment2.Configurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events", t =>
            {
                t.HasCheckConstraint("CK_Events_MaxAttendees", "[MaxAttendees] > 0");
                t.HasCheckConstraint("CK_Events_EndDate", "[EndDate] IS NULL OR [EndDate] >= [StartDate]");
            });

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Description)
                   .IsRequired();

            builder.Property(e => e.StartDate)
                   .IsRequired();

            builder.Property(e => e.MaxAttendees)
                   .IsRequired();

            builder.HasOne(e => e.Organizer)
                   .WithMany(e => e.Events)
                   .HasForeignKey(e => e.OrganizerId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
