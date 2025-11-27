using api.Context;
using api.Domain.Enuns;
using api.Domain.Factories;
using api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using tests.TestServices;

namespace tests.Pesquisa;

public class RepositoryTests
{
    
    private QuestionarioContext _context;
    private PesquisaRepository _pesquisaRepository;

    public RepositoryTests()
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>().UseInMemoryDatabase("RepositoryTests").Options;
        _context = new QuestionarioContext(options);
        
        _pesquisaRepository = new PesquisaRepository(_context);
    }
    
    
    [Fact]
    public async Task TestaIncluirPesquisaValida()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var result = await _pesquisaRepository.SalvarAsync(pesquisaEntidade);
        
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(StatusPesquisa.Criada, result.StatusPesquisa);
        PesquisaService.ComparaPesquisas(pesquisaEntidade, pesquisaDto);
    }
    
    [Fact]
    public async Task TestaObterPesquisaExistente()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var pesquisaSalva = await _pesquisaRepository.SalvarAsync(pesquisaEntidade);
        var pesquisaConsultaResult = await _pesquisaRepository.ObterPorIdAsync(pesquisaSalva.Id);
        
        Assert.NotEqual(Guid.Empty, pesquisaConsultaResult.Id);
        Assert.Equal(pesquisaSalva, pesquisaConsultaResult);
    }

    [Fact]
    public void TestaObterPesquisaInexistente()
    {
        var result = _pesquisaRepository.ObterPorIdAsync(Guid.Empty);
        var result2 = _pesquisaRepository.ObterPorIdAsync(Guid.NewGuid());
        Assert.Null(result);
        Assert.Null(result2);
    }
    
    [Fact]
    public async Task TestaAlterarPesquisaExistente()
    {
        var incluirPesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var incluirPesquisaEntidade = PesquisaFactory.CriarPesquisa(incluirPesquisaDto);
        
        var pesquisa = await _pesquisaRepository.SalvarAsync(incluirPesquisaEntidade);
        var pesquisaAlterada = pesquisa;

        pesquisaAlterada.Nome = "ABCD EEEEE KL";
        pesquisaAlterada.Perguntas[1].Enunciado = "TESTE TESTE TESTE";
        pesquisaAlterada.Perguntas[2].Alternativas[0].Texto = "Nada";

        var alterarPesquisaResult = await _pesquisaRepository.AtualizarAsync(pesquisa);
        
        Assert.NotNull(alterarPesquisaResult);
        Assert.NotEqual(Guid.Empty, alterarPesquisaResult.Id);
        Assert.Equal("ABCD EEEEE KL", alterarPesquisaResult.Nome);
        Assert.Equal("TESTE TESTE TESTE", alterarPesquisaResult.Perguntas[1].Enunciado);
        Assert.Equal("Nada", alterarPesquisaResult.Perguntas[2].Alternativas[0].Texto);
    }

    [Fact]
    public void TestaAlterarPesquisaRemovendoPergunta()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void TestaAlterarPesquisaRemovendoAlternativaDePergunta()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void TestaAlterarPesquisaInexistente()
    {
        var pesquisa = new api.Domain.Model.Pesquisa();
        var result = _pesquisaRepository.AtualizarAsync(pesquisa);
        
        Assert.Null(result);
    }

    [Fact]
    public async Task TestaListarTodos()
    {
        var tamanhoAnterior = _context.Pesquisas.Count();
        var registrosAInserir = 10;
        for (int i = 0; i < registrosAInserir; i++)
        {
            var incluirPesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
            var incluirPesquisaEntidade = PesquisaFactory.CriarPesquisa(incluirPesquisaDto);
        
            await _pesquisaRepository.SalvarAsync(incluirPesquisaEntidade);
        }
        
        var result = await _pesquisaRepository.ListarTodosAsync();
        
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(registrosAInserir + tamanhoAnterior, result.Count);
    }

    [Fact]
    public async Task TestaExcluirPesquisaExistente()
    {
        var consulta = await _pesquisaRepository.ListarTodosAsync();
        var quantidadeDeRegistrosAntesDaDelecao = consulta.Count;
        var entidade = await _pesquisaRepository.ListarTodosAsync();
        var result = await _pesquisaRepository.DeleteAsync(entidade.FirstOrDefault().Id);
        
        var consultaAposDelecao = await _pesquisaRepository.ListarTodosAsync();
        var registrosAposADelecao = consultaAposDelecao.Count;
        var registrosEsperadosAposADelecao = quantidadeDeRegistrosAntesDaDelecao - 1;
        
        Assert.Equal(registrosEsperadosAposADelecao, registrosAposADelecao);
        Assert.True(result);
    }

    [Fact]
    public async Task TestaExcluirPesquisaInexistente()
    {
        var entidade = new api.Domain.Model.Pesquisa();

        var consulta = await _pesquisaRepository.ListarTodosAsync();
        var quantidadeDeRegistrosAntesDaDelecao = consulta.Count;
        var result = await _pesquisaRepository.DeleteAsync(entidade.Id);

        consulta = await _pesquisaRepository.ListarTodosAsync();
        var registrosAposADelecao = consulta.Count;
        
        Assert.Equal(quantidadeDeRegistrosAntesDaDelecao, registrosAposADelecao);
        Assert.False(result);
    }
}