namespace SIGEBI.Domain.Abstractions
{
    public interface IUsuarioDomainRepository
    {
        Task<bool> ExistsActiveAsync(int usuarioId, CancellationToken ct = default);
        Task<bool> EmailExistsAsync(string email, int? excludingId, CancellationToken ct = default);
    }
}
