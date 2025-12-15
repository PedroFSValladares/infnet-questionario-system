namespace PesquisasStartup.Dominio.Repositorios.Base;

public interface IUpdatebleRepository<TEntity>
{
    public Task<TEntity?> AtualizarAsync(TEntity pesquisaDto);
}