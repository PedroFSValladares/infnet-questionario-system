namespace api.Domain.Interfaces;

public interface IUpdatebleRepository<in TId, TEntity>
{
    public TEntity? Atualizar(TId id, TEntity entity);
}