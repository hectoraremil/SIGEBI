namespace SIGEBI.Domain.Abstractions
{
    public interface IAuditoriaDomainRepository
    {
        Task<bool> ExistsActiveAsync(int auditoriaId, CancellationToken ct = default);
    }
}
