namespace api.Domain.Interfaces;

public interface IUpdatebleRepository<TEntity>
{
    public Task<TEntity?> AtualizarAsync(TEntity entity);
}