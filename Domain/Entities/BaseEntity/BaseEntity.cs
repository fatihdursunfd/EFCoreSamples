namespace Domain.Entities
{
    public class BaseEntity<T>
    {
        public T? Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}