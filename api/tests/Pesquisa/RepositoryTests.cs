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
    public void TestaIncluirPesquisaValida()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var result = _pesquisaRepository.Salvar(pesquisaEntidade);
        
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(StatusPesquisa.Criada, result.StatusPesquisa);
        PesquisaService.ComparaPesquisas(pesquisaEntidade, pesquisaDto);
    }
    
    [Fact]
    public void TestaObterPesquisaExistente()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var pesquisaSalva = _pesquisaRepository.Salvar(pesquisaEntidade);
        var pesquisaConsultaResult = _pesquisaRepository.ObterPorId(pesquisaSalva.Id);
        
        Assert.NotEqual(Guid.Empty, pesquisaConsultaResult.Id);
        Assert.Equal(pesquisaSalva, pesquisaConsultaResult);
    }

    [Fact]
    public void TestaObterPesquisaInexistente()
    {
        var result = _pesquisaRepository.ObterPorId(Guid.Empty);
        var result2 = _pesquisaRepository.ObterPorId(Guid.NewGuid());
        Assert.Null(result);
        Assert.Null(result2);
    }
    
    [Fact]
    public void TestaAlterarPesquisaExistente()
    {
        var incluirPesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var incluirPesquisaEntidade = PesquisaFactory.CriarPesquisa(incluirPesquisaDto);
        
        var pesquisa = _pesquisaRepository.Salvar(incluirPesquisaEntidade);
        var pesquisaAlterada = pesquisa;

        pesquisaAlterada.Nome = "ABCD EEEEE KL";
        pesquisaAlterada.Perguntas[1].Enunciado = "TESTE TESTE TESTE";
        pesquisaAlterada.Perguntas[2].Alternativas[0].Texto = "Nada";

        var alterarPesquisaResult = _pesquisaRepository.Atualizar(pesquisa);
        
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
        var result = _pesquisaRepository.Atualizar(pesquisa);
        
        Assert.Null(result);
    }

    [Fact]
    public void TestaListarTodos()
    {
        var tamanhoAnterior = _context.Pesquisas.Count();
        var registrosAInserir = 10;
        for (int i = 0; i < registrosAInserir; i++)
        {
            var incluirPesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
            var incluirPesquisaEntidade = PesquisaFactory.CriarPesquisa(incluirPesquisaDto);
        
            _pesquisaRepository.Salvar(incluirPesquisaEntidade);
        }
        
        var result = _pesquisaRepository.ListarTodos();
        
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(registrosAInserir + tamanhoAnterior, result.Count);
    }

    [Fact]
    public void TestaExcluirPesquisaExistente()
    {
        var quantidadeDeRegistrosAntesDaDelecao = _pesquisaRepository.ListarTodos().Count;
        var entidade = _pesquisaRepository.ListarTodos().FirstOrDefault();
        var result = _pesquisaRepository.Delete(entidade.Id);
        
        var registrosAposADelecao = _pesquisaRepository.ListarTodos().Count;
        var registrosEsperadosAposADelecao = quantidadeDeRegistrosAntesDaDelecao - 1;
        
        Assert.Equal(registrosEsperadosAposADelecao, registrosAposADelecao);
        Assert.True(result);
    }

    [Fact]
    public void TestaExcluirPesquisaInexistente()
    {
        var entidade = new api.Domain.Model.Pesquisa();
        
        var quantidadeDeRegistrosAntesDaDelecao = _pesquisaRepository.ListarTodos().Count;
        var result = _pesquisaRepository.Delete(entidade.Id);
        
        var registrosAposADelecao = _pesquisaRepository.ListarTodos().Count;
        
        Assert.Equal(quantidadeDeRegistrosAntesDaDelecao, registrosAposADelecao);
        Assert.False(result);
    }
}