namespace PesquisasStartup.Dominio.Entidades.Pesquisas;

public class Cpf
{
    public string Value { get; private set; }

    private Cpf(string value) => Value = value;

    public static implicit operator Cpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf.Trim()))
            throw new ArgumentNullException(paramName: nameof(cpf), message: "O cpf não pode conter um nome vazio ou nulo.");

        if (!CpfValido(cpf))
            throw new ArgumentException("O cpf informado não é válido.");
        
        return new Cpf(cpf);
    }
    public static implicit operator string(Cpf cpf) => cpf.Value;
    
    private static Boolean CpfValido(string cpf)
    {
        if (cpf.All(c => c == cpf[0]))
            return false;
        
        int primeiroDigitoVerificador;
        int segundoDigitoVerificador;
        string digitosVerificadoresOriginais = cpf.Substring(9);
        string digitoVerificadorFinal;

        char[] temp = new char[9];
        
        temp = cpf.ToCharArray(0, 9);
        
        primeiroDigitoVerificador = CalcularDigitoVerificador(temp);

        temp = cpf.ToCharArray(0, 10);
        temp[9] = primeiroDigitoVerificador.ToString().ToCharArray()[0];

        segundoDigitoVerificador = CalcularDigitoVerificador(temp);

        digitoVerificadorFinal = string.Concat(primeiroDigitoVerificador, segundoDigitoVerificador);

        return digitosVerificadoresOriginais.Equals(digitoVerificadorFinal);
    }
    
    private static int CalcularDigitoVerificador(char[] caracteres){
        int somatorio = 0,
            digitoVerificador;
        int[] sequencia = new int[caracteres.Length];

        for(int i = 0; i < caracteres.Length; i++)
            sequencia[i] = (int) Char.GetNumericValue(caracteres[i]);


        for(int i = 0,  peso = sequencia.Length + 1; i < sequencia.Length; i++, peso--){
            somatorio += sequencia[i] * peso;
        }

        int restoPrimeiraDivisao = somatorio % 11;
        digitoVerificador = restoPrimeiraDivisao < 2 ? 0 : 11 - restoPrimeiraDivisao;

        return digitoVerificador;
    }
}