using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration.Location
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Translations)
                .WithOne()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Timezones)
                .WithOne()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.States)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasMany(x => x.Proviences)
               .WithOne(x => x.Country)
               .HasForeignKey(x => x.CountryId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}