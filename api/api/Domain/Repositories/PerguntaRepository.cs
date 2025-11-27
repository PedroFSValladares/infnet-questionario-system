using api.Context;
using api.Domain.Interfaces;
using api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Domain.Repositories;

public class PerguntaRepository : 
    IIncludableRepository<Pergunta>, IQueryableRepository<Guid, Pergunta>, IUpdatebleRepository<Pergunta>, IDeletableRepository<Guid>
{
    private readonly QuestionarioContext _context;
    
    public PerguntaRepository(QuestionarioContext context)
    {
        _context = context;
    }
    
    public async Task<Pergunta> SalvarAsync(Pergunta entity)
    {
        var result = await _context.Perguntas.AddAsync(entity);
        return result.Entity;
    }

    public async Task<Pergunta?> ObterPorIdAsync(Guid id)
    {
        return await _context.Perguntas
            .Where(p => p.Id == id)
            .Include(pergunta => pergunta.Alternativas)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Pergunta>> ObterTodosAsync()
    {
        return  await _context.Perguntas.ToListAsync();
    }

    public async Task<Pergunta?> AtualizarAsync(Pergunta pesquisaDto)
    {
        var pergunta = await ObterPorIdAsync(pesquisaDto.Id);

        if (pergunta == null)
            return null;
         
        pergunta.Enunciado = pesquisaDto.Enunciado;
        pergunta.Alternativas = pesquisaDto.Alternativas;
        
        return pergunta;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var pergunta = await ObterPorIdAsync(id);
        
        if (pergunta == null) return false;
        
        _context.Perguntas.Remove(pergunta);
        return true;
    }

    public async Task CommitChanges()
    {
        await _context.SaveChangesAsync();
    }
}