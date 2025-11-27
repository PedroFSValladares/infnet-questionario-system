using api.Context;
using api.Domain.Enuns;
using api.Domain.Factories;
using api.Domain.Model;
using api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using tests.TestServices;

namespace tests.Pesquisa;

public class RepositoryTests : IAsyncLifetime
{
    
    private QuestionarioContext _context;
    private PesquisaRepository _pesquisaRepository;

    public RepositoryTests()
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>().UseInMemoryDatabase("RepositoryTests").Options;
        _context = new QuestionarioContext(options);
        
        _pesquisaRepository = new PesquisaRepository(_context);
    }

    public QuestionarioContext GetContext(string name)
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>().UseInMemoryDatabase(name)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;
        return new QuestionarioContext(options);
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.DisposeAsync();
    }
    
    [Fact]
    public async Task TestaIncluirPesquisaValida()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = new api.Domain.Model.Pesquisa()
        {
            Nome = "teste",
            Perguntas = new List<Pergunta>()
            {
                new Pergunta()
                {
                    Enunciado = "teste",
                    Alternativas = new List<Alternativa>()
                    {
                        new Alternativa()
                        {
                            Opcao = 'A',
                            Texto = "Altenativa A"
                        }
                    }
                }
            }
        };

        await pesquisaRepository.SalvarAsync(pesquisa);
        
        var result = await context.Pesquisas.FindAsync(pesquisa.Id);
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task TestaObterPesquisaExistente()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = new api.Domain.Model.Pesquisa()
        {
            Nome = "teste",
            Perguntas = new List<Pergunta>()
            {
                new Pergunta()
                {
                    Enunciado = "teste",
                    Alternativas = new List<Alternativa>()
                    {
                        new Alternativa()
                        {
                            Opcao = 'A',
                            Texto = "Altenativa A"
                        }
                    }
                }
            }
        };
        await context.Pesquisas.AddAsync(pesquisa);
        await context.SaveChangesAsync();
        
        var pesquisaConsultaResult = await pesquisaRepository.ObterPorIdAsync(pesquisa.Id);
        
        Assert.NotNull(pesquisaConsultaResult);
        Assert.NotEmpty(pesquisaConsultaResult.Perguntas);
        Assert.NotEmpty(pesquisaConsultaResult.Perguntas[0].Alternativas);
    }

    [Fact]
    public async Task TestaObterPesquisaInexistente()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = new api.Domain.Model.Pesquisa();
        
        await context.Pesquisas.AddAsync(pesquisa);
        
        var result = await pesquisaRepository.ObterPorIdAsync(Guid.Empty);
        var result2 = await pesquisaRepository.ObterPorIdAsync(Guid.NewGuid());
        
        Assert.Null(result);
        Assert.Null(result2);
    }
    
    [Fact]
    public async Task TestaAlterarDadosPesquisaExistente()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = new api.Domain.Model.Pesquisa()
        {
            Nome = "teste",
        };
        await context.Pesquisas.AddAsync(pesquisa);
        await context.SaveChangesAsync();

        var pesquisaAlterada = new api.Domain.Model.Pesquisa(pesquisa.Id)
        {
            Nome = "teste 2",
        };
        
        await pesquisaRepository.AtualizarAsync(pesquisaAlterada);
        
        var result = await context.Pesquisas.FindAsync(pesquisa.Id);
        Assert.NotNull(result);
        Assert.Equal(pesquisaAlterada, result);
    }

    [Fact]
    public async Task TestaAlterarPesquisaRemovendoPergunta()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = new api.Domain.Model.Pesquisa()
        {
            Nome = "Teste"
        };
        
        await context.Pesquisas.AddAsync(pesquisa);
        await context.SaveChangesAsync();
        
        pesquisa.Perguntas.Add(new Pergunta(){Enunciado = "Pergunta Teste"});
    }

    [Fact]
    public void TestaAlterarPesquisaRemovendoAlternativaDePergunta()
    {
        throw new NotImplementedException();
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