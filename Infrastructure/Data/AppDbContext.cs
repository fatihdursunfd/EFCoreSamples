using Application.Interfaces;
using Domain.Dtos.Location;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options), IAppDbContext
    {

        #region DbSet

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Provience> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }

        #endregion

        #region Compiled Queries

        /// <summary>
        /// Gönderilen id'ye ait ülke bilgisini çekmek için kullanılır.
        /// </summary>
        public async Task<CountryDto?> GetCountryByIdAsync(int id, Language language)
            => await CompiledQueries.GetCountryByIdQuery(this, id, language);

        /// <summary>
        /// Bütün ülke bilgilerini çekmek için kullanılır.
        /// </summary>
        public async Task<List<CountryDto>> GetAllCountriesAsync(Language language)
        {
            var countries = new List<CountryDto>();
            await foreach (var country in CompiledQueries.GetAllCountriesQuery(this, language))
            {
                countries.Add(country);
            }
            return countries;
        }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Db'ye yapılan ve FullAuditableEntity tipinde olan bütün tablolardan çekilen verilere default bir tenant ve isDeleted filtrelemesi eklenir.
        /// Veriyi çekerken bu filtrelemenin ignore edilmesi istenilirse IgnoreQueryFilter kullanılır.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Mevcut assembly içerisinde bulunan configuration dosyalarında bulunan konfigurasyonlar uygulanır
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Enum, DateOnly ve TimeOnly gibi tiplerin db'ye eklenirken otomatik olarak configure edilir.
        /// </summary>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>()
                .HaveColumnType("tinyint");

            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            configurationBuilder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>()
                .HaveColumnType("time");

            base.ConfigureConventions(configurationBuilder);
        }

        
    }
}