using Microsoft.EntityFrameworkCore;
using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Repositorios.Especializacoes;
using PesquisasStartup.Infraestrutura.Context;

namespace PesquisasStartup.Infraestrutura.Repositorios;

public class PesquisaRepository : IPesquisaRepository
{
    
    private readonly PesquisasStartupContext _context;

    public PesquisaRepository(PesquisasStartupContext context)
    {
        _context = context;
    }
    
    public async Task SalvarAsync(Pesquisa entity)
    {
        _context.Pesquisas.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Pesquisa pesquisa)
    {
        _context.Pesquisas.Remove(pesquisa);
        await _context.SaveChangesAsync();
    }

    public async Task<Pesquisa?> ObterPorIdAsync(Guid id)
    {
        return await _context.Pesquisas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);;
    }

    public async Task AtualizarAsync(Pesquisa pesquisa)
    {
        var pesquisaBanco = await _context.Pesquisas.FindAsync(pesquisa.Id);

        if (pesquisaBanco != null)
        {
            pesquisaBanco.AtualizarNome(pesquisa.Nome);
            
            foreach (var perguntaBanco in pesquisaBanco.Perguntas)
            {
                if(pesquisa.Perguntas.Contains(perguntaBanco)) continue;
                pesquisaBanco.RemoverPergunta(perguntaBanco.Enunciado);
            }
            
            foreach (var pergunta in pesquisa.Perguntas)
            {
                if (pesquisaBanco.Perguntas.Contains(pergunta)) continue;
                var alternativas = pergunta.Alternativas.Select(a => (a.Opcao, a.Texto)).ToList();
                pesquisaBanco.AdicionarPergunta(pergunta.Enunciado, alternativas);
            }

            await _context.SaveChangesAsync();
        }
    }

    public Task<List<Pesquisa>> ListarTodosAsync()
    {
        return _context.Pesquisas.AsNoTracking().ToListAsync();
    }
}