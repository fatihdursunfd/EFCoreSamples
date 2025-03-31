using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration.Location
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("Districts");

            builder.HasIndex(x => x.Id);

            builder
                .HasOne(x => x.Provience)
                .WithMany(x => x.Districts)
                .HasForeignKey(x => x.ProvienceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}