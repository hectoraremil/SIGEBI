namespace SIGEBI.Domain.Abstractions
{
    public interface IDevolucionDomainRepository
    {
        Task<bool> ExistsActiveAsync(int devolucionId, CancellationToken ct = default);
        Task<bool> ExistsByPrestamoIdAsync(int prestamoId, CancellationToken ct = default);
    }
}
