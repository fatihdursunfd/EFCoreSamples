using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration.Location
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");

            builder.HasKey(x => x.Id);  

            builder
                .HasOne(x => x.Country)
                .WithMany(x => x.States)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Proviences)
                .WithOne(x => x.State)
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}