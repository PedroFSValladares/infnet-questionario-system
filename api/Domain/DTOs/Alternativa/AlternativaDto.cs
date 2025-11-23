using System.ComponentModel.DataAnnotations;

namespace api.Domain.DTOs.Alternativa;

public class AlternativaDto
{
    [Required(ErrorMessage = "Devem ser informado uma opção para a alternativa.")]
    public char Opcao { get; set; }
    [Required(ErrorMessage = "A alternativa deve conter um texto.")]
    public string Texto { get; set; }

    public virtual Model.Alternativa ToEntity()
    {
        return new Model.Alternativa(Opcao, Texto);
    }
}