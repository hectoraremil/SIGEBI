namespace SIGEBI.Domain.Base
{
    public abstract class AuditableEntity<TKey> : Entity<TKey>
    {
        public DateTime CreationDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public int CreationUser { get; set; }
        public int? UserMod { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? UserDeleted { get; set; }
    }
}
