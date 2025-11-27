namespace api.Domain.Interfaces;

public interface IDeletableRepository<in TId> : ICommitableRepository
{
    public Task<bool> DeleteAsync(TId id);
}