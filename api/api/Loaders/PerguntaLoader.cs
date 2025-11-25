using api.Domain.Model;
using api.Domain.Repositories;

namespace api.Loaders;

public class PerguntaLoader : BackgroundService
{
    
    private readonly ILogger<PerguntaLoader> _logger;
    private readonly IServiceProvider _serviceProvider;

    public PerguntaLoader(ILogger<PerguntaLoader> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Iniciando carga de perguntas...");

        string[] linhas = File.ReadAllLines(Path.Combine("Dados", "Perguntas.tsv"));
        bool pularCabecalho = true;

        foreach (var linha in linhas)
        {
            string[] campos = linha.Split('\t');
            if(pularCabecalho)
                pularCabecalho = false;
            else
            {
                List<Alternativa> alternativas = new List<Alternativa>
                {
                    new Alternativa
                    {
                        Opcao = 'A',
                        Texto =  campos[1],
                    },
                    new Alternativa
                    {
                        Opcao = 'B',
                        Texto =  campos[2],
                    },
                    new Alternativa
                    {
                        Opcao = 'C',
                        Texto =  campos[3],
                    },
                    new Alternativa
                    {
                        Opcao = 'D',
                        Texto =  campos[4],
                    },
                    new Alternativa
                    {
                        Opcao = 'E',
                        Texto =  campos[5],
                    }
                };
                using (var scope = _serviceProvider.CreateScope())
                {
                    Pergunta pergunta = new Pergunta
                    {
                        Enunciado = campos[0],
                        Alternativas = alternativas
                    };
                    PerguntaRepository repository = scope.ServiceProvider.GetRequiredService<PerguntaRepository>(); 
                    repository.Salvar(pergunta);
                    _logger.LogInformation($"Pergunta: {pergunta}");
                }
            }
        }
        _logger.LogInformation("Carga de perguntas finalizada.");
        return Task.CompletedTask;
    }
}