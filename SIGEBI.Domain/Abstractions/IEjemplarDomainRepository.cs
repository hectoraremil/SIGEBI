namespace SIGEBI.Domain.Abstractions
{
    public interface IEjemplarDomainRepository
    {
        Task<bool> ExistsActiveAsync(int ejemplarId, CancellationToken ct = default);
        Task<bool> CodigoExistsAsync(string codigo, int? excludingId, CancellationToken ct = default);
        Task<bool> TienePrestamoActivoAsync(int ejemplarId, CancellationToken ct = default);
    }
}
