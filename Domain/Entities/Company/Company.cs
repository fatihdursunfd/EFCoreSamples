using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Company : BaseEntity<int>
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public int EmployeeCount { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}