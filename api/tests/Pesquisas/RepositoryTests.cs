using api.Context;
using api.Domain.Enuns;
using api.Domain.Factories;
using api.Domain.Model;
using api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using tests.TestServices;

namespace tests.Pesquisas;

public class RepositoryTests : IAsyncLifetime
{
    
    [Fact]
    public async Task TestaIncluirPesquisaValida()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = ObterPesquisaTeste();

        await pesquisaRepository.SalvarAsync(pesquisa);
        await pesquisaRepository.CommitChanges();
        
        context.ChangeTracker.Clear();
        
        var result = await context.Pesquisas.FindAsync(pesquisa.Id);
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task TestaObterPesquisaExistente()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = ObterPesquisaTeste();
        await context.Pesquisas.AddAsync(pesquisa);
        await context.SaveChangesAsync();
        
        context.ChangeTracker.Clear();
        
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
        var pesquisa = ObterPesquisaTeste();
        
        await context.Pesquisas.AddAsync(pesquisa);
        
        context.ChangeTracker.Clear();
        
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
        var pesquisa = ObterPesquisaTeste();
        await context.Pesquisas.AddAsync(pesquisa);
        await context.SaveChangesAsync();
        
        context.ChangeTracker.Clear();

        var pesquisaEditada = ObterPesquisaTesteComIdPreenchidos(pesquisa);
        pesquisaEditada.Nome = "teste 2";
        pesquisaEditada.Perguntas[0].Enunciado = "enunciado 2";
        pesquisaEditada.Perguntas[0].Alternativas[0].Texto = "Gelo";
        await pesquisaRepository.AtualizarAsync(pesquisaEditada);
        await pesquisaRepository.CommitChanges();
        
        context.ChangeTracker.Clear();

        var result = await context.Pesquisas
            .Where(r => r.Id == pesquisaEditada.Id)
            .Include(p => p.Perguntas)
            .ThenInclude(p => p.Alternativas)
            .FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal(pesquisaEditada.Nome, result.Nome);
        Assert.Equal(pesquisaEditada.Perguntas[0].Enunciado, result.Perguntas[0].Enunciado);
        Assert.Equal(pesquisaEditada.Perguntas[0].Alternativas[0].Texto, result.Perguntas[0].Alternativas[0].Texto);
    }

    [Fact]
    public async Task TestaAlterarPesquisaRemovendoPergunta()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = ObterPesquisaTeste();
        await context.Pesquisas.AddAsync(pesquisa);
        await context.SaveChangesAsync();
        
        context.ChangeTracker.Clear();

        var pesquisaEditada = ObterPesquisaTesteComIdPreenchidos(pesquisa);
        pesquisaEditada.Perguntas.RemoveAt(1);
        await pesquisaRepository.AtualizarAsync(pesquisaEditada);
        await pesquisaRepository.CommitChanges();
        
        context.ChangeTracker.Clear();
        
        var result = await context.Pesquisas
            .Where(r => r.Id == pesquisaEditada.Id)
            .Include(p => p.Perguntas)
            .ThenInclude(p => p.Alternativas)
            .FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal(pesquisaEditada.Perguntas.Count, result.Perguntas.Count);
        Assert.Equal(pesquisaEditada.Perguntas.Count, pesquisa.Perguntas.Count - 1);
    }

    [Fact]
    public async Task TestaAlterarPesquisaAdicionandoPergunta()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = ObterPesquisaTeste();
        await context.Pesquisas.AddAsync(pesquisa);
        await context.SaveChangesAsync();
        
        context.ChangeTracker.Clear();
        var pesquisaEditada = ObterPesquisaTesteComIdPreenchidos(pesquisa);
        pesquisaEditada.Perguntas.Add(new Pergunta()
        {
            Enunciado = "abcd",
            PesquisaId = pesquisaEditada.Id,
            Alternativas = new List<Alternativa>()
            {
                new Alternativa(Guid.NewGuid())
                {
                    Opcao = 'D',
                    Texto = "abcde",
                }
            }
        });
        await pesquisaRepository.AtualizarAsync(pesquisaEditada);
        await pesquisaRepository.CommitChanges();
        
        context.ChangeTracker.Clear();
        
        var result = await context.Pesquisas
            .Where(r => r.Id == pesquisaEditada.Id)
            .Include(p => p.Perguntas)
            .ThenInclude(p => p.Alternativas)
            .FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal(pesquisaEditada.Perguntas.Count, result.Perguntas.Count);
        Assert.Equal(pesquisaEditada.Perguntas.Count,  pesquisa.Perguntas.Count + 1);
    }

    [Fact]
    public async Task TestaAlterarPesquisaRemovendoAlternativaDePergunta()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = ObterPesquisaTeste();
        await context.Pesquisas.AddAsync(pesquisa);
        await context.SaveChangesAsync();
        
        context.ChangeTracker.Clear();
        
        context.ChangeTracker.Clear();
        var pesquisaEditada = ObterPesquisaTesteComIdPreenchidos(pesquisa);
        pesquisaEditada.Perguntas[0].Alternativas.RemoveAt(0);
        await pesquisaRepository.AtualizarAsync(pesquisaEditada);
        await pesquisaRepository.CommitChanges();
        
        context.ChangeTracker.Clear();
        
        var result = await context.Pesquisas
            .Where(r => r.Id == pesquisaEditada.Id)
            .Include(p => p.Perguntas)
            .ThenInclude(p => p.Alternativas)
            .FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal(pesquisaEditada.Perguntas[0].Alternativas.Count, result.Perguntas[0].Alternativas.Count);
        Assert.Equal(pesquisaEditada.Perguntas[0].Alternativas.Count,  pesquisa.Perguntas[0].Alternativas.Count - 1);
    }

    [Fact]
    public async Task TestaAlterarPesquisaAdicionandoAlternativaDePergunta()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = ObterPesquisaTeste();
        await context.Pesquisas.AddAsync(pesquisa);
        await context.SaveChangesAsync();
        
        context.ChangeTracker.Clear();
        
        var pesquisaEditada = ObterPesquisaTesteComIdPreenchidos(pesquisa);
        pesquisaEditada.Perguntas[0].Alternativas.Add(new Alternativa()
        {
            Opcao = 'D',
            Texto = "abcde",
        });
        await pesquisaRepository.AtualizarAsync(pesquisaEditada);
        await pesquisaRepository.CommitChanges();
        
        context.ChangeTracker.Clear();
        
        var result = await context.Pesquisas
            .Where(r => r.Id == pesquisaEditada.Id)
            .Include(p => p.Perguntas)
            .ThenInclude(p => p.Alternativas)
            .FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal(pesquisaEditada.Perguntas[0].Alternativas.Count, result.Perguntas[0].Alternativas.Count);
        Assert.Equal(pesquisaEditada.Perguntas[0].Alternativas.Count,  pesquisa.Perguntas[0].Alternativas.Count + 1);
    }

    [Fact]
    public async Task TestaListarTodos()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var quantidadeDeRegistrosAAdicionar = 10;
        for (var i = 0; i < quantidadeDeRegistrosAAdicionar; i++)
        {
            var pesquisa = ObterPesquisaTeste();
            await context.Pesquisas.AddAsync(pesquisa);
        }
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        
        var result = await pesquisaRepository.ListarTodosAsync();
        
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(quantidadeDeRegistrosAAdicionar, result.Count);
    }

    [Fact]
    public async Task TestaExcluirPesquisa()
    {
        var context = GetContext(Guid.NewGuid().ToString());
        var pesquisaRepository = new PesquisaRepository(context);
        var pesquisa = ObterPesquisaTeste();
        context.Pesquisas.Add(pesquisa);
        await context.SaveChangesAsync();
        
        var retorno = await pesquisaRepository.DeleteAsync(pesquisa.Id);
        await pesquisaRepository.CommitChanges();
        
        context.ChangeTracker.Clear();
        
        var result = await context.Pesquisas.FindAsync(pesquisa.Id);
        Assert.True(retorno);
        Assert.Null(result);
    }
    
    private QuestionarioContext GetContext(string name)
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>().UseInMemoryDatabase(name)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;
        return new QuestionarioContext(options);
    }

    private Pesquisa ObterPesquisaTeste()
    {
        return new api.Domain.Model.Pesquisa(Guid.NewGuid())
        {
            Nome = "teste",
            Perguntas = new List<Pergunta>()
            {
                new Pergunta(Guid.NewGuid())
                {
                    Enunciado = "teste",
                    Alternativas = new List<Alternativa>()
                    {
                        new Alternativa(Guid.NewGuid())
                        {
                            Opcao = 'A',
                            Texto = "Altenativa A"
                        }
                    }
                },
                new Pergunta(Guid.NewGuid())
                {
                    Enunciado = "teste",
                    Alternativas = new List<Alternativa>()
                    {
                        new Alternativa(Guid.NewGuid())
                        {
                            Opcao = 'A',
                            Texto = "Altenativa A"
                        }
                    }
                }
            }
        };
    }

    private Pesquisa ObterPesquisaTesteComIdPreenchidos(Pesquisa pesquisaACopiar)
    {
        var pesquisaCopiada = ObterPesquisaTeste();
        
        pesquisaCopiada.Id = pesquisaACopiar.Id;
        pesquisaCopiada.Perguntas = pesquisaCopiada.Perguntas.Select((pergunta, index) =>
        {
            pergunta.Id = pesquisaACopiar.Perguntas[index].Id;
            pergunta.Alternativas = pergunta.Alternativas
                .Select((alternativa, i) =>
                {
                    alternativa.Id = pergunta.Alternativas[i].Id;
                    return alternativa;
                }).ToList();
            return pergunta;
        }).ToList();
        
        return pesquisaCopiada;
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
