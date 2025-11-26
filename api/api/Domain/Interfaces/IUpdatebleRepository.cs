namespace api.Domain.Interfaces;

public interface IUpdatebleRepository<TEntity>
{
    public TEntity? Atualizar(TEntity entity);
}