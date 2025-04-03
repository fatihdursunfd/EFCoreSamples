using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seed
{
    public static class DataSeeder
    {
        /// <summary>
        /// Başlangıçta hiçbir kullanıcı olmadığı durumda, seed olarak bir rol ve user oluşturmak için kullanılır
        /// </summary>
        public static async Task Seed(AppDbContext? _context, CancellationToken cancellationToken)
        {
            if (_context == null)
            {
                return;
            }

            _context.Database.EnsureCreated();


            DateTime now = DateTime.UtcNow.Date;

            var userExists = await _context.Users.AnyAsync(x => x.Email == "fatih.dursun.616@gmail.com", cancellationToken);

            if (userExists)
            {
                return;
            }

            await _context.Roles
                .AddAsync(new() { Name = "SuperUser", NormalizedName = "SUPERUSER", CreatedBy = Guid.Empty, CreatedDate = now, IsDeleted = false }, cancellationToken);

            await _context.Users
                .AddAsync(new()
                {
                    Name = "FATIH",
                    Surname = "DURSUN",
                    Email = "fatih.dursun.616@gmail.com",
                    AccessFailedCount = 0,
                    CreatedBy = Guid.Empty,
                    CreatedDate = now,
                    IsDeleted = false,
                    UserName = "fatihdursun"
                }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}