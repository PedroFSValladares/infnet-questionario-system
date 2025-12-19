using Microsoft.EntityFrameworkCore;
using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Infraestrutura.Context;
using PesquisasStartup.Infraestrutura.Repositorios;

namespace PesquisasStartup.Infraestrutra.Testes;

public class TestesPesquisaRepository
{
    
    private PesquisasStartupContext _context;
    private PesquisaRepository _pesquisaRepository;

    private PesquisasStartupContext GetContext()
    {
        return new PesquisasStartupContext(new DbContextOptionsBuilder<PesquisasStartupContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
    }

    private Pesquisa ObterPesquisa()
    {
        return Pesquisa.CriarPesquisa(
            "PesquisaXPTO", 
            new List<(string, List<(char opcao, string texto)>)>
            {
                ("Pergunta 1", new List<(char, string)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" ),
                }),
                ("Pergunta 2", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" ),
                })
            });
    }
    
    [Fact]
    public async Task TestaSalvarPesquisa_DeveObterSucesso()
    {
        _context = GetContext();
        _pesquisaRepository = new PesquisaRepository(_context);
        var pesquisa = ObterPesquisa();

        await _pesquisaRepository.SalvarAsync(pesquisa);
        var result = await _context.Pesquisas
                .Include(p => p.Perguntas)
                .Include(p => p.Respostas)
                .Include(p => p.Situacoes)
                .FirstOrDefaultAsync(p => p.Id == pesquisa.Id);
        
        Assert.Equal(pesquisa, result);
    }
    
    [Fact]
    public async Task TestaObterPesquisaPeloId_DeveObterSucesso()
    {
        _context = GetContext();
        _pesquisaRepository = new PesquisaRepository(_context);
        var pesquisa = ObterPesquisa();
        
        _context.Pesquisas.Add(pesquisa);
        await _context.SaveChangesAsync();
        
        var result = await _pesquisaRepository.ObterPorIdAsync(pesquisa.Id);

        Assert.Equal(pesquisa, result);
    }
    
    [Fact]
    public async Task TestaListarPesquisas_DeveObterSucesso()
    {
        _context = GetContext();
        _pesquisaRepository = new PesquisaRepository(_context);
        var pesquisas = new List<Pesquisa>();

        for (int i = 0; i < 10; i++)
            pesquisas.Add(ObterPesquisa());
        
        await _context.AddRangeAsync(pesquisas);
        await _context.SaveChangesAsync();

        var result = await _pesquisaRepository.ListarTodosAsync();
        
        Assert.Equal(10, result.Count);
        Assert.True(result.All(p => 
            pesquisas.Any(p2 => p2.Id == p.Id)));
        Assert.True(result.All(p => 
            pesquisas.Any(p2 => p2.Nome == p.Nome)));
    }
    
    [Fact]
    public async Task TestaDeletarPesquisa_DeveObterSucesso()
    {
        _context = GetContext();
        _pesquisaRepository = new PesquisaRepository(_context);
        var pesquisa = ObterPesquisa();
        
        _context.Pesquisas.Add(pesquisa);
        await _context.SaveChangesAsync();

        await _pesquisaRepository.DeleteAsync(pesquisa);
        
        var result = _context.Pesquisas.ToList();
        
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task TestaAlterarPesquisa_DeveObterSucesso()
    {
        _context = GetContext();
        _pesquisaRepository = new PesquisaRepository(_context);
        var pesquisa = ObterPesquisa();
        
        _context.Pesquisas.Add(pesquisa);
        await _context.SaveChangesAsync();
        
        pesquisa.AtualizarNome("Pesquisa Falsa");

        var novaPergunta = Pergunta.CriarPergunta("Pergunta 3", new List<(char, string)>
        {
            ('A', "Alternativa A"),
            ('B', "Alternativa B")
        });
        pesquisa.AdicionarPergunta(novaPergunta.Enunciado,
            novaPergunta.Alternativas.Select(pergunta => (pergunta.Opcao, pergunta.Texto)).ToList());
        
        var perguntaAlterada = pesquisa.Perguntas[1];
        perguntaAlterada.AdicionarAlternativa('E', "Alternativa E");
        perguntaAlterada.RemoverAlternativa('A', "Alternativa A");
        pesquisa.RemoverPergunta(perguntaAlterada.Enunciado);
        pesquisa.AdicionarPergunta(perguntaAlterada.Enunciado,
            perguntaAlterada.Alternativas.Select(pergunta => (pergunta.Opcao, pergunta.Texto)).ToList());
        
        pesquisa.RemoverPergunta("Pergunta 1");
        pesquisa.PublicarPesquisa();

        await _pesquisaRepository.AtualizarAsync(pesquisa);
        
        var result = await _context.Pesquisas
            .Include(p => p.Perguntas)
            .Include(p => p.Respostas)
            .Include(p => p.Situacoes)
            .FirstOrDefaultAsync(p => p.Id == pesquisa.Id);
        
        Assert.Equal(pesquisa, result);
    }
}