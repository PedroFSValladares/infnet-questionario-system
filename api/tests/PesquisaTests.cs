using System.Net;
using api.Context;
using api.Controllers;
using api.Domain.DTOs.Alternativa;
using api.Domain.DTOs.Pergunta;
using api.Domain.DTOs.Pesquisa;
using api.Domain.Enuns;
using api.Domain.Model;
using api.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tests.Factories;

namespace tests;

public class PesquisaTests
{
    private QuestionarioContext _context;
    private PesquisaRepository _pesquisaRepository;
    private PesquisaController _pesquisaController;

    public PesquisaTests()
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>().UseInMemoryDatabase("QuestionarioContext").Options;
        _context = new QuestionarioContext(options);
        
        _pesquisaRepository = new PesquisaRepository(_context);
        _pesquisaController = new PesquisaController(_pesquisaRepository);
    }
    
    [Fact]
    public void ConverteIncluirPesquisaDtoParaEntidade()
    {
        var pesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
        var pesquisaEntidade = pesquisaDto.ToEntity();
        
        Assert.NotNull(pesquisaEntidade);
        ComparaPesquisas(pesquisaEntidade, pesquisaDto);
    }

    [Fact]
    public void ConverteAlterarPesquisaDtoParaEntidade()
    {
        throw new NotImplementedException(); //TODO
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
        ComparaPesquisas(pesquisaEntidade, pesquisaDto);
    }

    [Fact]
    public void TestaIncluirPesquisaController()
    {
        var pesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
        
        var result = _pesquisaController.Incluir(pesquisaDto);
        Assert.IsType<CreatedResult>(result);
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
    public void TestaObterPesquisaController()
    {
        var pesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
        var pesquisaEntidade = pesquisaDto.ToEntity();
        
        var pesquisaSalva = _pesquisaRepository.Salvar(pesquisaEntidade);
        
        var consultaResult = _pesquisaController.ObterPorId(pesquisaSalva.Id);
        Assert.IsType<OkObjectResult>(consultaResult);
    }

    [Fact]
    public void TestaObterPesquisaInexistenteController()
    {
        var consultaResult = _pesquisaController.ObterPorId(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(consultaResult);
    }

    [Fact]
    public void TestaAlterarPesquisaRepositorio()
    {
        throw new NotImplementedException(); //TODO
    }

    [Fact]
    public void TestaAlterarPesquisaController()
    {
        throw new NotImplementedException(); //TODO
    }

    [Fact]
    public void TestaAlterarPesquisaInexistenteController()
    {
        throw new NotImplementedException(); //TODO
    }
    
    private void ComparaPesquisas(Pesquisa pesquisaEntidade, IncluirPesquisaDto pesquisaDto)
    {
        Assert.Equal(pesquisaDto.Nome, pesquisaEntidade.Nome);
        Assert.All(pesquisaEntidade.Perguntas, (pergunta, index) =>
        {
            ComparaPerguntas(pesquisaDto.Perguntas[index], pergunta);
        });
    }
    
    private void ComparaPerguntas(IncluirPerguntaDto perguntaDto,
        Pergunta perguntaEntidade)
    {
        Assert.Equal(perguntaDto.Enunciado, perguntaEntidade.Enunciado);
        Assert.All(perguntaDto.Alternativas, (alternativaDto, i) =>
        {
            Assert.Equal(alternativaDto.Opcao, perguntaEntidade.Alternativas[i].Opcao);
            Assert.Equal(alternativaDto.Texto, perguntaEntidade.Alternativas[i].Texto);
        });
    }
}
