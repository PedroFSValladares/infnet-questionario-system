using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Repositorios.Pesquisa;

namespace PesquisasStartup.Infraestrutura.Repositorios;

public class PesquisaRepository : IPesquisaRepository
{
    public Task<Pesquisa> SalvarAsync(Pesquisa entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Pesquisa?> ObterPorIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Pesquisa?> AtualizarAsync(Pesquisa pesquisaDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<Pesquisa>> ListarTodosAsync()
    {
        throw new NotImplementedException();
    }
}