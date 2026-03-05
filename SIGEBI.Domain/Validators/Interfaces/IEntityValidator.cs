namespace SIGEBI.Domain.Validators.Interfaces
{
    public interface IEntityValidator<TEntity, TId>
    {
        Task ValidateForCreateAsync(TEntity entity, CancellationToken ct = default);
        Task ValidateForUpdateAsync(TEntity entity, CancellationToken ct = default);
        Task ValidateForDeleteAsync(TId id, int userId, CancellationToken ct = default);
    }
}
