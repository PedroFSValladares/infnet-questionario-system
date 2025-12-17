using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Entidades.Pessoas;

public class Pessoa
{
    public Cpf Cpf { get; private set; }
    public string Nome { get; private set; }
    public Perfil Perfil { get; private set; }

    private Pessoa(string cpf)
    {
        Cpf = cpf;
    }

    public static Pessoa CriarPessoa(string cpf, string nome, Perfil perfil)
    {
        var pessoa = new Pessoa(cpf);
        pessoa.AtualizarNome(nome);
        pessoa.AtualizarPerfil(perfil);
        return pessoa;
    }
    
    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrEmpty(nome.Trim()))
            throw new ArgumentNullException(nameof(nome), "O nome do usuário deve ser informado.");
        
        Nome = nome;
    }

    public void AtualizarPerfil(Perfil perfil)
    {
        Perfil = perfil;
    }
}