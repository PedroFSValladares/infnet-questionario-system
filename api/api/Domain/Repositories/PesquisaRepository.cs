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
    
    public Pesquisa Salvar(Pesquisa entity)
    {
        var pesquisa = _context.Pesquisas.Add(entity).Entity;
        _context.SaveChanges();
        return pesquisa;
    }

    public Pesquisa? ObterPorId(Guid id)
    {
        var pesquisa = _context.Pesquisas
            .Where(p => p.Id == id)
            .Include(p => p.Perguntas)
            .FirstOrDefault();
        return pesquisa;
    }
    
    public Pesquisa? Atualizar(Pesquisa entity)
    {
        var pesquisa = ObterPorId(entity.Id);
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
        _context.SaveChanges();
        return pesquisa;
    }

    public List<Pesquisa> ListarTodos()
    {
        return _context.Pesquisas.ToList();
    }

    public bool Delete(Guid id)
    {
        var pesquuisa = ObterPorId(id);
        
        if (pesquuisa == null) return false;
        
        _context.Pesquisas.Remove(pesquuisa);
        _context.SaveChanges();
        return true;
    }
}