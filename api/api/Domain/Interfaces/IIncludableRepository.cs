namespace api.Domain.Interfaces;

public interface IIncludableRepository<TEntity>
{
    public Task<TEntity> SalvarAsync(TEntity entity);
}