using api.Context;
using api.Domain.Enuns;
using api.Domain.Factories;
using api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using tests.TestServices;

namespace tests.Pesquisa.Repository;

public class RepositoryTests
{
    
    private QuestionarioContext _context;
    private PesquisaRepository _pesquisaRepository;

    public RepositoryTests()
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>().UseInMemoryDatabase("QuestionarioContext").Options;
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

        var alterarPesquisaResult = _pesquisaRepository.Atualizar(pesquisa.Id, pesquisa);
        
        Assert.NotNull(alterarPesquisaResult);
        Assert.NotEqual(Guid.Empty, alterarPesquisaResult.Id);
        Assert.Equal("ABCD EEEEE KL", alterarPesquisaResult.Nome);
        Assert.Equal("TESTE TESTE TESTE", alterarPesquisaResult.Perguntas[1].Enunciado);
        Assert.Equal("Nada", alterarPesquisaResult.Perguntas[2].Alternativas[0].Texto);
    }

    [Fact]
    public void TestaAlterarPesquisaInexistente()
    {
        var pesquisa = new api.Domain.Model.Pesquisa();
        var result = _pesquisaRepository.Atualizar(pesquisa.Id, pesquisa);
        
        Assert.Null(result);
    }
}