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
        var pesquisaInclusa = await _pesquisaRepository.SalvarAsync(pesquisa);
        await _pesquisaRepository.CommitChanges();
        return pesquisaInclusa ;
    }

    public async Task<Pesquisa?> AlterarPesquisa(Pesquisa pesquisaAAlterar)
    {
        var pesquisaAlterada = await _pesquisaRepository.AtualizarAsync(pesquisaAAlterar); 
        await _pesquisaRepository.CommitChanges();
        return pesquisaAlterada;
    }

    public async Task<List<Pesquisa>> ListarPesquisasAsync()
    {
        return await _pesquisaRepository.ListarTodosAsync();
    }

    public async Task<Pesquisa?> BuscarPesquisaPorId(Guid id)
    {
        return await _pesquisaRepository.ObterPorIdAsync(id);
    }

    public async Task<bool> ExcluirPesquisa(Guid id)
    {
        var deletionResult = await _pesquisaRepository.DeleteAsync(id); 
        await _pesquisaRepository.CommitChanges();
        return deletionResult;
    }
}