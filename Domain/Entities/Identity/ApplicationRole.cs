using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<Guid>, BaseEntity<Guid>
    {
        public DateTime CreatedDate {get; set;}
        public Guid? CreatedBy {get; set;}
        public DateTime? ModifiedDate {get; set;}
        public Guid? ModifiedBy {get; set;}
        public bool IsDeleted {get; set;}
    }
}