namespace api.Domain.DTOs.Alternativa;

public class IncluirAlternativaDto : AlternativaDto
{
    public virtual Model.Alternativa ToEntity()
    {
        return new Model.Alternativa(Opcao, Texto);
    }
}