using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data.QueryFilter
{
    public static class AddQueryFilters
    {
        /// <summary>
        /// BaseEntity'lere default olarak queryFilter'ın eklenmesi için kullanılır
        /// </summary>
        public static void AddQueryFilterToBaseEntities<T>(this ModelBuilder builder) where T : class
        {
            Type type = typeof(T);

            var baseTypes = Enumerable.Repeat(type.BaseType, 1).Concat(type.GetInterfaces()).Distinct().ToList();
            
            if (baseTypes.Contains(typeof(Entity)))
            {
                MethodInfo? queryFilterMethod = typeof(AddQueryFilters).GetMethod(nameof(AddQueryFilterToBaseEntity));

                if (queryFilterMethod is null)
                {
                    return;
                }

                queryFilterMethod = queryFilterMethod.MakeGenericMethod([typeof(T)]);
                queryFilterMethod.Invoke(null, [builder]);
            }
        }

        /// <summary>
        /// QueryFilter'ın BaseEntity'e eklenmesi için kullanılır
        /// </summary>
        private static void AddQueryFilterToBaseEntity<T>(ModelBuilder builder) where T : class, Entity
        {
            builder
                .Entity<T>()
                .HasQueryFilter(x => !x.IsDeleted);
        }
    }
}