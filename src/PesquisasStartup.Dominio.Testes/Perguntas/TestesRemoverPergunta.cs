using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Dominio.Testes.Perguntas;

public class TestesRemoverPergunta
{
    [Fact]
    public void TestaDeixarPerguntaComUmaAlternativa_DeveFalhar()
    {
        var pergunta = Pergunta.CriarPergunta("Pergunta 1", new List<(char opcao, string texto)>
        {
            ('a', "alternativa A"),
            ('b', "alternativa B")
        });
        
        Assert.Throws<InvalidOperationException>(() => pergunta.RemoverAlternativa('a', "alternativa A"));
    }
    
    [Fact]
    public void TestaRemoverAlternativaExistenteDePergunta_DeveObterSucesso()
    {
        var pergunta = Pergunta.CriarPergunta("Pergunta 1", new List<(char opcao, string texto)>
        {
            ('a', "alternativa A"),
            ('b', "alternativa B"),
            ('c', "alternativa C")
        });
        
        pergunta.RemoverAlternativa('a', "alternativa A");
        
        Assert.Equal(2, pergunta.Alternativas.Count);
        Assert.DoesNotContain(pergunta.Alternativas, alt => alt.Opcao == 'a');
    }
    
    [Fact]
    public void TestaRemoverAlternativaInexistenteDePergunta_DeveFalhar()
    {
        var pergunta = Pergunta.CriarPergunta("Pergunta 1", new List<(char opcao, string texto)>
        {
            ('a', "alternativa A"),
            ('b', "alternativa B"),
            ('c', "alternativa C")
        });
        
        Assert.Throws<InvalidOperationException>(() => pergunta.RemoverAlternativa('d',  "alternativa A"));
    }
}