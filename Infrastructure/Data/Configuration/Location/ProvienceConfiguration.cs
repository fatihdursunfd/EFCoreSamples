using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration.Location
{
    public class ProvienceConfiguration : IEntityTypeConfiguration<Provience>
    {
        public void Configure(EntityTypeBuilder<Provience> builder)
        {
            builder.ToTable("Proviences");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.CountryId)
                .IsRequired(false);

            builder
                .HasOne(x => x.Country)
                .WithMany(x => x.Proviences)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
               .Property(x => x.StateId)
               .IsRequired(false);

            builder
                .HasOne(x => x.State)
                .WithMany(x => x.Proviences)
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Districts)
                .WithOne(x => x.Provience)
                .HasForeignKey(x => x.ProvienceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}