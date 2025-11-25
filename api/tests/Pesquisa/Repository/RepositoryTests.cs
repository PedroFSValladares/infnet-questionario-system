using api.Context;
using api.Controllers;
using api.Domain.Enuns;
using api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using tests.Factories;
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
    public void TestaIncluirPesquisaRespositorio()
    {
        var pesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
        var pesquisaEntidade = pesquisaDto.ToEntity();
        
        var result = _pesquisaRepository.Salvar(pesquisaEntidade);
        
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(StatusPesquisa.Criada, result.StatusPesquisa);
        PesquisaService.ComparaPesquisas(pesquisaEntidade, pesquisaDto);
    }
    
    [Fact]
    public void TestaObterPesquisaRepository()
    {
        var pesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
        var pesquisaEntidade = pesquisaDto.ToEntity();
        
        var pesquisaSalva = _pesquisaRepository.Salvar(pesquisaEntidade);
        var pesquisaConsultaResult = _pesquisaRepository.ObterPorId(pesquisaSalva.Id);
        
        Assert.Equal(pesquisaSalva, pesquisaConsultaResult);
    }
    
    [Fact]
    public void TestaAlterarPesquisaRepositorio()
    {
        throw new NotImplementedException(); //TODO
    }
}