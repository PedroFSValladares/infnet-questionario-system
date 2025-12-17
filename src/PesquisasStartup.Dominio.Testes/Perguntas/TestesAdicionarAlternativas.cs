using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Dominio.Testes.Perguntas;

public class TestesAdicionarAlternativas
{
    [Fact]
    public void TestaAdicionarAlternativaJaExistente_DeveFalhar()
    {
        var pergunta = Pergunta.CriarPergunta("Pergunta 1", new List<(char opcao, string texto)>
        {
            ('a', "alternativa A"),
            ('b', "alternativa B"),
            ('c', "alternativa C")
        });
        
        Assert.Throws<InvalidOperationException>(() => pergunta.AdicionarAlternativa('a',  "alternativa A"));
    }
}