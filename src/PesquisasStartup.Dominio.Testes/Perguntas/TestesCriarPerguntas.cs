using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Dominio.Testes.Perguntas;

public class TestesCriarPerguntas
{
    [Fact]
    public void TestaCriarPerguntaValida_DeveObterSucesso()
    {
        var pergunta = Pergunta.CriarPergunta("Pergunta 1", new List<(char opcao, string texto)>
        {
            ('a', "alternativa A"),
            ('b', "alternativa B")
        });

        Assert.NotNull(pergunta);
        Assert.Equal("Pergunta 1", pergunta.Enunciado);
        Assert.Equal(2, pergunta.Alternativas.Count);
        Assert.Contains(pergunta.Alternativas, alt => alt.Opcao == 'a');
        Assert.Contains(pergunta.Alternativas, alt => alt.Opcao == 'b');
    }
    
    [Fact]
    public void TestaCriarPerguntaComEnunciadoVazio_DeveFalhar()
    {
        Assert.Throws<ArgumentNullException>(() => Pergunta.CriarPergunta("", new List<(char opcao, string texto)>
            {
                ('a', "alternativa A"),
                ('b', "alternativa B")
            })
        );
    }
    
    [Fact]
    public void TestaCriarPerguntaComAlternativaComOpcaoRepetida_DeveFalhar()
    {
        Assert.Throws<InvalidOperationException>(() => Pergunta.CriarPergunta("Pergunta 1", new List<(char opcao, string texto)>
            {
                ('a', "alternativa A"),
                ('a', "alternativa B")
            })
        );
    }
    
    [Fact]
    public void TestaCriarPerguntaComAlternativaComApenasUmaOpcao_DeveFalhar()
    {
        Assert.Throws<ArgumentException>(() => Pergunta.CriarPergunta("Pergunta 1", new List<(char opcao, string texto)>
            {
                ('a', "alternativa A")
            })
        );
    }
}