using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data.Configuration
{
    public static class ConfigureBaseEntities
    {
        /// <summary>
        /// BaseEntity içerisindeki alanların configure edilmesi için kullanılır
        /// </summary>
        public static void ConfigureEntities<T>(this ModelBuilder builder) where T : class
        {
            Type type = typeof(T);

            List<Type> baseTypes = Enumerable.Repeat(type.BaseType, 1).Concat(type.GetInterfaces()).Distinct().ToList();

            if (baseTypes.Contains(typeof(Entity)))
            {
                Type? baseType = baseTypes.FirstOrDefault(x => x == typeof(Entity));
                if (baseType is null)
                {
                    return;
                }

                PropertyInfo[] properties = baseType.GetProperties();
                MethodInfo? configureMethod = typeof(ConfigureBaseEntities).GetMethod(nameof(ConfigureBaseEntity));

                if (configureMethod is null)
                {
                    return;
                }

                configureMethod = configureMethod.MakeGenericMethod([typeof(T)]);
                configureMethod.Invoke(null, [builder, baseType, properties]);
            }
        }

        /// <summary>
        /// BaseEntity'e ait prop'ların configure edilmesi için kullanılır
        /// </summary>
        private static void ConfigureBaseEntity<T>(this ModelBuilder builder, Type baseType, PropertyInfo[] properties) where T : class
        {
            foreach (var prop in properties)
            {
                string columnType = string.Empty;
                bool isRequired = false;

                switch (prop.Name)
                {
                    case nameof(Entity.CreatedDate):
                        columnType = "datetime";
                        isRequired = true;
                        break;

                    case nameof(Entity.CreatedBy):
                        columnType = "nvarchar(36)";
                        isRequired = true;
                        break;

                    case nameof(Entity.ModifiedDate):
                        columnType = "datetime";
                        isRequired = false;
                        break;

                    case nameof(Entity.ModifiedBy):
                        columnType = "nvarchar(36)";
                        isRequired = false;
                        break;

                    case nameof(Entity.IsDeleted):
                        columnType = "bit";
                        isRequired = true;
                        break;
                }

                builder
                    .Entity<T>()
                    .Property(prop.Name)
                    .IsRequired(isRequired)
                    .HasColumnType(columnType);

                builder
                    .Entity<T>()
                    .HasIndex(["IsDeleted"]);
            }
        }

    }
}