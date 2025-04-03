using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, BaseEntity<Guid>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public DateTime CreatedDate {get; set;}
        public Guid? CreatedBy {get; set;}
        public DateTime? ModifiedDate {get; set;}
        public Guid? ModifiedBy {get; set;}
        public bool IsDeleted {get; set;}
    }
}