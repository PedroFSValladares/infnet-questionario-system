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
        return await _context.Pesquisas
            .Include(p => p.Respostas)
            .Include(p => p.Situacoes)
            .Include(p => p.Perguntas)
            .ThenInclude(pergunta => pergunta.Alternativas)
            .FirstOrDefaultAsync(x => x.Id == id);;
    }

    public async Task AtualizarAsync(Pesquisa pesquisa)
    {
        _context.Pesquisas.Update(pesquisa);
        await _context.SaveChangesAsync();
    }

    public Task<List<Pesquisa>> ListarTodosAsync()
    {
        return _context.Pesquisas.AsNoTracking().ToListAsync();
    }
}