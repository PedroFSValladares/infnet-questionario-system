using api.Context;
using api.Domain.Interfaces;
using api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Domain.Repositories;

public class PesquisaRepository : IIncludableRepository<Pesquisa>, IUpdatebleRepository<Pesquisa>, IListableRepository<Pesquisa>, IDeletableRepository<Guid>
{
    
    private readonly QuestionarioContext _context;
    
    public PesquisaRepository(QuestionarioContext context)
    {
        _context = context;
    }
    
    public async Task<Pesquisa> SalvarAsync(Pesquisa entity)
    {
        var pesquisa = await _context.Pesquisas.AddAsync(entity);
        await _context.SaveChangesAsync();
        return pesquisa.Entity;
    }

    public async Task<Pesquisa?> ObterPorIdAsync(Guid id)
    {
        var pesquisa = await _context.Pesquisas
            .Where(p => p.Id == id)
            .Include(p => p.Perguntas)
            .FirstOrDefaultAsync();
        return pesquisa;
    }
    
    public async Task<Pesquisa?> AtualizarAsync(Pesquisa entity)
    {
        var pesquisa = await ObterPorIdAsync(entity.Id);
        if (pesquisa == null) return null;

        pesquisa.Nome = entity.Nome;
        pesquisa.StatusPesquisa = entity.StatusPesquisa;
        pesquisa.DataDispnibilizacao = entity.DataDispnibilizacao;
        pesquisa.DataFinalizacao = entity.DataFinalizacao;
        
        var perguntasARemover = pesquisa.Perguntas
            .Where(p => !entity.Perguntas.Any(e => e.Id == p.Id))
            .ToList();

        foreach (var pergunta in perguntasARemover)
        {
            pesquisa.Perguntas.Remove(pergunta);
        }

        foreach (var pergunta in entity.Perguntas)
        {
            var perguntaExistente = pesquisa.Perguntas.FirstOrDefault(p => p.Id == pergunta.Id);

            if (perguntaExistente != null)
            {
                perguntaExistente.Enunciado = pergunta.Enunciado;
                
                var alternativasARemover = perguntaExistente.Alternativas
                    .Where(a => !pergunta.Alternativas.Any(x => x.Id == a.Id))
                    .ToList();
                
                foreach (var alternativa in alternativasARemover)
                    pergunta.Alternativas.Remove(alternativa);

                foreach (var alternativa in pergunta.Alternativas)
                {
                    var alternativaExistente = perguntaExistente.Alternativas.FirstOrDefault(a => a.Id == alternativa.Id);

                    if (alternativaExistente != null)
                    {
                        alternativaExistente.Opcao = alternativa.Opcao;
                        alternativaExistente.Texto = alternativa.Texto;
                    }
                    else
                    {
                        perguntaExistente.Alternativas.Add(alternativa);
                    }
                }
            }
            else
            {
                pesquisa.Perguntas.Add(pergunta);
            }
        }
        
        _context.Pesquisas.Update(pesquisa);
        await _context.SaveChangesAsync();
        return pesquisa;
    }

    public async Task<List<Pesquisa>> ListarTodosAsync()
    {
        return await _context.Pesquisas.ToListAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var pesquuisa = await ObterPorIdAsync(id);
        
        if (pesquuisa == null) return false;
        
        _context.Pesquisas.Remove(pesquuisa);
        await _context.SaveChangesAsync();
        return true;
    }
}