namespace Domain.Entities
{
    public interface BaseEntity<T> : Entity
    {
        /// <summary>
        /// Generic id alanı
        /// </summary>
        public T Id { get; set; }
    }

    public interface Entity
    {
        /// <summary>
        /// Verinin oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Verinin kim tarafından oluşturulduğu
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Verinin son güncellenme tarihi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Verinin son kim tarafından güncellendiği
        /// </summary>
        public Guid? ModifiedBy { get; set; }

        /// <summary>
        /// Silindi mi?
        /// </summary>
        public bool IsDeleted { get; set; }
    }

}