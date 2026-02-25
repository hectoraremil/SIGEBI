namespace SIGEBI.Domain.Base
{
    public abstract class AuditableEntity<TKey> : Entity<TKey>
    {
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifyDate { get; set; }

        public int CreationUser { get; set; } = 1;
        public int? UserMod { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? UserDeleted { get; set; }

        public void MarkCreated(int userId)
        {
            CreationUser = userId;
            CreationDate = DateTime.UtcNow;
        }

        public void MarkModified(int userId)
        {
            UserMod = userId;
            ModifyDate = DateTime.UtcNow;
        }

        public void SoftDelete(int userId)
        {
            Deleted = true;
            UserDeleted = userId;
            DeletedDate = DateTime.UtcNow;
        }
    }
}
