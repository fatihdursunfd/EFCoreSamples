using Application.Interfaces.Data;
using Domain.Dtos.Location;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Enums;
using Infrastructure.Data.Configuration;
using Infrastructure.Data.QueryFilter;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,
                                                  ApplicationRole,
                                                  Guid,
                                                  ApplicationUserClaim,
                                                  ApplicationUserRole,
                                                  ApplicationUserLogin,
                                                  ApplicationRoleClaim,
                                                  ApplicationUserToken>, IAppDbContext
    {

        private readonly ICurrentUser _currentUser;
        public AppDbContext(DbContextOptions options, ICurrentUser currentUser) : base(options)
        {
            _currentUser = currentUser;
        }

        #region DbSet

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        public DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Provience> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }

        public DbSet<Company> Companies { get; set; }

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
            DateTime now = DateTime.UtcNow;

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUser.UserId;
                        entry.Entity.CreatedDate = now;
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = _currentUser.UserId;
                        entry.Entity.ModifiedDate = now;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.ModifiedBy = _currentUser.UserId;
                        entry.Entity.ModifiedDate = now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Db'ye yapılan ve FullAuditableEntity tipinde olan bütün tablolardan çekilen verilere default bir tenant ve isDeleted filtrelemesi eklenir.
        /// Veriyi çekerken bu filtrelemenin ignore edilmesi istenilirse IgnoreQueryFilter kullanılır.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var entityTypes = builder.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                MethodInfo? addQueryFilterMethod = typeof(AddQueryFilters)
                    .GetMethod(nameof(AddQueryFilters.AddQueryFilterToBaseEntities));

                if (addQueryFilterMethod is null)
                {
                    continue;
                }

                addQueryFilterMethod = addQueryFilterMethod.MakeGenericMethod([entityType.ClrType]);
                addQueryFilterMethod.Invoke(null, [builder]);

                MethodInfo? configurationMethod = typeof(ConfigureBaseEntities)
                    .GetMethod(nameof(ConfigureBaseEntities.ConfigureEntities));

                if (configurationMethod is null)
                {
                    continue;
                }

                configurationMethod = configurationMethod.MakeGenericMethod([entityType.ClrType]);
                configurationMethod.Invoke(null, [builder]);
            }

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