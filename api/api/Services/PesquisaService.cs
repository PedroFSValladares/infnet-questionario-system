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

    public async Task<Pesquisa> IncluirPesquisaAsync(Pesquisa pesquisa)
    {
        return await _pesquisaRepository.SalvarAsync(pesquisa);
    }

    public async Task<Pesquisa?> AlterarPesquisaAsync(Pesquisa pesquisaAAlterar)
    {
        return await _pesquisaRepository.AtualizarAsync(pesquisaAAlterar);
    }

    public async Task<List<Pesquisa>> ListarPesquisasAsync()
    {
        return await _pesquisaRepository.ListarTodosAsync();
    }

    public async Task<Pesquisa?> BuscarPesquisaPorIdAsync(Guid id)
    {
        return await _pesquisaRepository.ObterPorIdAsync(id);
    }

    public async Task<bool> ExcluirPesquisaAsync(Guid id)
    {
        return await _pesquisaRepository.DeleteAsync(id);
    }
}