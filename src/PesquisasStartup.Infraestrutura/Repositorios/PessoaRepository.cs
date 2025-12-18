using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Repositorios.Especializacoes;

namespace PesquisasStartup.Infraestrutura.Repositorios;

public class PessoaRepository : IUsuarioRepository
{
    public Task SalvarAsync(Pessoa entity)
    {
        throw new NotImplementedException();
    }

    public Task AtualizarAsync(Pessoa pesquisaDto)
    {
        throw new NotImplementedException();
    }

    public Task<Pessoa?> ObterPorIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}