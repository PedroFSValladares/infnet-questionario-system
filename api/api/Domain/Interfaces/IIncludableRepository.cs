namespace api.Domain.Interfaces;

public interface IIncludableRepository<TEntity> : ICommitableRepository
{
    public Task<TEntity> SalvarAsync(TEntity entity);
}