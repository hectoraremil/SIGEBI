namespace SIGEBI.Domain.Abstractions
{
    public interface IPrestamoDomainRepository
    {
        Task<bool> ExistsActiveAsync(int prestamoId, CancellationToken ct = default);
        Task<bool> EjemplarDisponibleAsync(int ejemplarId, CancellationToken ct = default);
        Task<bool> UsuarioTienePrestamoActivoAsync(int usuarioId, int ejemplarId, CancellationToken ct = default);
    }
}
