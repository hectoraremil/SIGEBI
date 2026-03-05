namespace SIGEBI.Domain.Abstractions
{
    public interface IPenalizacionDomainRepository
    {
        Task<bool> ExistsActiveAsync(int penalizacionId, CancellationToken ct = default);
        Task<bool> UsuarioTienePenalizacionActivaAsync(int usuarioId, CancellationToken ct = default);
    }
}
