namespace api.Domain.Interfaces;

public interface IIncludableRepository<TEntity>
{
    public TEntity Salvar(TEntity entity);
}