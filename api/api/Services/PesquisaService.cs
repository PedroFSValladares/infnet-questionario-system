using api.Domain.Model;
using api.Domain.Repositories;

namespace api.Services;

public class PesquisaService
{
    private readonly PesquisaRepository _pesquisaRepository;

    public PesquisaService(PesquisaRepository pesquisaRepository)
    {
        _pesquisaRepository = pesquisaRepository;
    }

    public Pesquisa IncluirPesquisa(Pesquisa pesquisa)
    {
        return _pesquisaRepository.Salvar(pesquisa);
    }

    public Pesquisa? AlterarPesquisa(Pesquisa pesquisaAAlterar)
    {
        return _pesquisaRepository.Atualizar(pesquisaAAlterar);
    }

    public List<Pesquisa> ListarPesquisas()
    {
        return _pesquisaRepository.ListarTodos();
    }

    public Pesquisa? BuscarPesquisaPorId(Guid id)
    {
        return _pesquisaRepository.ObterPorId(id);
    }

    public void ExcluirPesquisa(Guid id)
    {
        _pesquisaRepository.Delete(id);
    }
}