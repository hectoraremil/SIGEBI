namespace SIGEBI.Domain.Abstractions
{
    public interface IRolDomainRepository
    {
        Task<bool> ExistsActiveAsync(int rolId, CancellationToken ct = default);
        Task<bool> NombreExistsAsync(string nombre, int? excludingId, CancellationToken ct = default);
    }
}
