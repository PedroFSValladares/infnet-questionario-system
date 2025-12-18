namespace PesquisasStartup.Dominio.Repositorios.Base;

public interface IDeletableRepository<in TEntity>
{
    public Task DeleteAsync(TEntity id);
}