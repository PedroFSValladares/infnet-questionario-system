namespace api.Domain.Interfaces;

public interface IQueryableRepository<in TId, TEntity>
{
    public TEntity? ObterPorId(TId id);
    public List<TEntity> ObterTodos();
}