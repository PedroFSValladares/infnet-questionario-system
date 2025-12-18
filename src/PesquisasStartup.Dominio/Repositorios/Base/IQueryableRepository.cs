namespace PesquisasStartup.Dominio.Repositorios.Base;

public interface IQueryableRepository<in TId, TEntity>
{
    public Task<TEntity?> ObterPorIdAsync(TId id);
}