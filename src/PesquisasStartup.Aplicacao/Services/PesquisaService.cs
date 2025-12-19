using Microsoft.Extensions.Logging;
using PesquisasStartup.Aplicacao.Dtos;
using PesquisasStartup.Aplicacao.Extensions;
using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Enums;
using PesquisasStartup.Dominio.Repositorios.Especializacoes;

namespace PesquisasStartup.Aplicacao.Services;

public class PesquisaService
{
    private readonly IPesquisaRepository _pesquisaRepository;
    private readonly ILogger<PesquisaService> _logger;

    public PesquisaService(IPesquisaRepository pesquisaRepository, ILogger<PesquisaService> logger)
    {
        _logger = logger;
        _pesquisaRepository = pesquisaRepository;
    }

    public async Task<PesquisaDto?> SalvarPesquisaAsync(PesquisaDto pesquisaDto)
    {
        Pesquisa pesquisa;
        
        try
        {
            pesquisa = Pesquisa.CriarPesquisa(pesquisaDto.Nome, pesquisaDto.Perguntas);
        }
        catch (Exception e)
        {
            _logger.LogError("Erro de domínio ao criar a pesquisa: {}", e.Message);
            return null;
        }
        
        await _pesquisaRepository.SalvarAsync(pesquisa);
        
        return pesquisa.ToPesquisaDto();
    }

    public async Task<PesquisaDto?> AlterarPesquisaAsync(PesquisaDto pesquisaDto)
    {
        var pesquisa = await _pesquisaRepository.ObterPorIdAsync(pesquisaDto.Id);

        if (pesquisa == null)
        {
            _logger.LogError("Pesquisa não encontrada: {}", pesquisaDto);
            return null;
        }
        
        var perguntasAAdicionar = pesquisaDto.Perguntas.Where(p =>
            pesquisa.Perguntas.All(p2 => p.enunciado != p2.Enunciado)) ;
        var perguntaARemover = pesquisa.Perguntas.Where(p =>
            pesquisaDto.Perguntas.All(p2 => p.Enunciado != p2.enunciado));

        try
        {
            pesquisa.AtualizarNome(pesquisaDto.Nome);
            foreach (var pergunta in perguntasAAdicionar)
                pesquisa.AdicionarPergunta(pergunta.enunciado, pergunta.alternvativas);
            foreach (var pergunta in perguntaARemover)
                pesquisa.RemoverPergunta(pergunta.Enunciado);
        }
        catch (Exception e)
        {
            _logger.LogError("Erro de domínio ao tentar alterar a pesquisa {0}: {1}", pesquisaDto.Id, e.Message);
            return null;
        }

        return pesquisa.ToPesquisaDto();
    }

    public async Task ExcluirPesquisaAsync(Guid id)
    {
        var pesquisa = await _pesquisaRepository.ObterPorIdAsync(id);

        if (pesquisa.Situacoes.Last().TipoSituacao > TipoSituacaoPesquisa.EmProducao)
        {
            _logger.LogError("A pesquisa só pode ser apagada enquanto está em produção.");
            throw new InvalidOperationException("A pesquisa só pode ser apagada enquanto está em produção.");
        }
        
        await _pesquisaRepository.DeleteAsync(pesquisa);
    }

    public async Task<PesquisaDto?> ObterPesquisaPorIdAsync(Guid id)
    {
        var pesquisa = await _pesquisaRepository.ObterPorIdAsync(id);
        
        return pesquisa?.ToPesquisaDto();
    }

    public async Task<List<PesquisaDto>> ObterTodosAsync()
    {
        var pesquisas = await _pesquisaRepository.ListarTodosAsync(); 
        return pesquisas.Select(p => p.ToPesquisaDto()).ToList();
    }

    public async Task<PesquisaDto?> PublicarAsync(PesquisaDto pesquisaDto)
    {
        var pesquisa = await _pesquisaRepository.ObterPorIdAsync(pesquisaDto.Id);
        if (pesquisa == null)
            return null;
        
        try
        {
            pesquisa.PublicarPesquisa();
        }
        catch (Exception e)
        {
            _logger.LogError("Erro de domínio ao tentar publicar a pesquisa {0}: {1}", pesquisaDto.Id, e.Message);
            return null;
        }
        
        return pesquisa.ToPesquisaDto();
    }

    public async Task<PesquisaDto?> FinalizarAsync(PesquisaDto pesquisaDto)
    {
        var pesquisa = await _pesquisaRepository.ObterPorIdAsync(pesquisaDto.Id);
        if (pesquisa == null)
            return null;
        
        try
        {
            pesquisa.FinalizarPesquisa();
        }
        catch (Exception e)
        {
            _logger.LogError("Erro de domínio ao tentar finalizar a pesquisa {0}: {1}", pesquisaDto.Id, e.Message);
            return null;
        }
        
        return pesquisa.ToPesquisaDto();
    }

    public async Task RespoderPesquisaAsync(RespostaDto respostaDto)
    {
        var pesquisa = await _pesquisaRepository.ObterPorIdAsync(respostaDto.IdPesquisa);

        if (pesquisa == null)
            throw new InvalidOperationException("Pesquisa informada não existe.");
        try
        {
            pesquisa.Responder(respostaDto.Cpf, respostaDto.Respostas);
        }
        catch (Exception e)
        {
            _logger.LogError("Erro de dominio ao tentar respodner a pesquisa {0}: {1}", pesquisa.Id, e.Message);
            throw;
        }
    }

    public async Task<ResultadoPesquisaDto> ObterResultadoPesquisaAsync(Guid id)
    {
        var pesquisa = await _pesquisaRepository.ObterPorIdAsync(id);

        pesquisa.Respostas.Select(resposta => new ResultadoPesquisaDto()
        {
            IdPesquisa = pesquisa.Id,
            NomePesquisa = pesquisa.Nome
        });
        
        throw new  NotImplementedException(); //TODO
    }
}