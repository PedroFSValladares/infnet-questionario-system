using System.Text.Json;
using api.Domain.DTOs.Pesquisa;

namespace tests;

public static class Exemplos
{
    private static string jsonBruto = File.ReadAllText("ExemplosDTO.json");

    public static List<IncluirPesquisaDto> ObterExemplosValidos()
    {
        var exemplos = JsonSerializer.Deserialize<DTOExemplos>(jsonBruto);
        return exemplos.Validos;
    }

    public static List<IncluirPesquisaDto> ObterExemplosInvalidos()
    {
        var exemplos = JsonSerializer.Deserialize<DTOExemplos>(jsonBruto);
        return exemplos.Invalidos;
    }

    class DTOExemplos
    {
        public List<IncluirPesquisaDto> Validos { get; set; }
        public List<IncluirPesquisaDto> Invalidos { get; set; }
    }
}