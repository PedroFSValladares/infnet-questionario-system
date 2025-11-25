using api.Context;
using api.Domain.Interfaces;
using api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Domain.Repositories;

public class PesquisaRepository : IIncludableRepository<Pesquisa>, IUpdatebleRepository<Guid, Pesquisa>
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
    
    public Pesquisa? Atualizar(Guid id, Pesquisa entity)
    {
        var pesquisa = ObterPorId(id);
        if (pesquisa == null) return null;

        pesquisa.Nome = entity.Nome;
        pesquisa.Perguntas = entity.Perguntas;
        pesquisa.StatusPesquisa = entity.StatusPesquisa;
        pesquisa.DataDispnibilizacao = entity.DataDispnibilizacao;
        pesquisa.DataFinalizacao = entity.DataFinalizacao;
        
        _context.Pesquisas.Update(pesquisa);
        _context.SaveChanges();
        return pesquisa;
    }
}