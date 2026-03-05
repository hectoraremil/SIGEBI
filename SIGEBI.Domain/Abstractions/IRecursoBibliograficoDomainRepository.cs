namespace SIGEBI.Domain.Abstractions
{
    public interface IRecursoBibliograficoDomainRepository
    {
        Task<bool> ExistsActiveAsync(int recursoId, CancellationToken ct = default);
        Task<bool> IsbnExistsAsync(string isbn, int? excludingId, CancellationToken ct = default);
    }
}
