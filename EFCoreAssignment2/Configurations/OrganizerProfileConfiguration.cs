using EFCoreAssignment2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreAssignment2.Configurations
{
    internal class OrganizerProfileConfiguration : IEntityTypeConfiguration<OrganizerProfile>
    {
        public void Configure(EntityTypeBuilder<OrganizerProfile> builder)
        {
            builder.ToTable("OrganizerProfiles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Biography)
                   .HasMaxLength(1000);

            builder.Property(x => x.WebsiteUrl)
                   .HasMaxLength(300);

            builder.Property(x => x.LogoUrl)
                   .HasMaxLength(500);

            builder.HasOne(x => x.Organizer)
                   .WithOne(p => p.Profile)
                   .HasForeignKey<OrganizerProfile>(p => p.OrganizerId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
