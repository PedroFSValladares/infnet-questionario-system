using api.Context;
using api.Domain.Factories;
using api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using tests.TestServices;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace tests.Pesquisas;

public class FactoryTests
{
    
    private QuestionarioContext _context;

    public FactoryTests()
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>().UseInMemoryDatabase("FactoryTests").Options;
        _context = new QuestionarioContext(options);
    }
    
    [Fact]
    public void TestaCriarEntidadeAPartirDeIncluiPesquisaDtoValido()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        Assert.NotNull(pesquisaEntidade);
        Assert.NotEqual(Guid.Empty, pesquisaEntidade.Id);
        PesquisaService.ComparaPesquisas(pesquisaEntidade, pesquisaDto);
    }

    [Fact]
    public void TestaCriarEntidadeAPartirDeIncluiPesquisaDtoInvalido()
    {
        Assert.Throws<ArgumentNullException>(() => PesquisaFactory.CriarPesquisa(Exemplos.ObterExemplosInvalidos().FirstOrDefault()));
    }

    [Fact]
    public void TestaCriarEntidadeAPartirDeAlterarPesquisaDtoValido()
    {
        var pesquisaIncluirDto =  Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaIncluirDto);
        
        var inclusaoResult = _context.Pesquisas.Add(pesquisaEntidade).Entity;
        _context.SaveChanges();
        
        var alterarPesquisaDto = PesquisaConverter.ConverterEntidadeParaAlterarPesquisaDto(inclusaoResult);
        var alterarPesquisaEntidade = PesquisaFactory.CriarPesquisa(alterarPesquisaDto);
        
        Assert.Equal(inclusaoResult.Id, alterarPesquisaEntidade.Id);
        Assert.Equal(inclusaoResult.Nome, alterarPesquisaEntidade.Nome);
        Assert.Equal(inclusaoResult.Perguntas.Count, alterarPesquisaEntidade.Perguntas.Count);
        Assert.All(alterarPesquisaEntidade.Perguntas, (pergunta, index) =>
        {
            Assert.Equal(pergunta.Id, inclusaoResult.Perguntas[index].Id);
            Assert.Equal(pergunta.Enunciado, inclusaoResult.Perguntas[index].Enunciado);
            Assert.Equal(pergunta.Alternativas, pergunta.Alternativas);
        });
    }
}