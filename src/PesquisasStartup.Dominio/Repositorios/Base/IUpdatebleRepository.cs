namespace PesquisasStartup.Dominio.Repositorios.Base;

public interface IUpdatebleRepository<TEntity>
{
    public Task AtualizarAsync(TEntity pesquisaDto);
}