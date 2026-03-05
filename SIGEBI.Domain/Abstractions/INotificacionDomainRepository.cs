namespace SIGEBI.Domain.Abstractions
{
    public interface INotificacionDomainRepository
    {
        Task<bool> ExistsActiveAsync(int notificacionId, CancellationToken ct = default);
    }
}
