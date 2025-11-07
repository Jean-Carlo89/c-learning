using BankSystem.API.model;

public class Conta
{
    public int Numero { get; set; }
    public decimal Saldo { get; set; }
    public string Titular { get; set; }

    public string Tipo { get; set; }

    //   public DateTime DataCriacao { get; set; }
    //   public string Situacao { get; set; }

    public Conta(int Numero, decimal Saldo, string Tipo, string Titular, DateTime DataCriacao, string Situacao)
    {
        this.Numero = Numero;
        this.Saldo = Saldo;
        this.Tipo = Tipo;
        this.Titular = Titular;
        // / / this.DataCriacao = DateTime.Now;
        // / this.Situacao = "Ativa";
    }

    // public Conta ConverterParaViewModel(BankAccount conta)
    // {


    //     // A conta 'conta' seria o resultado de uma busca no banco de dados.
    //     return new Conta
    //     {
    //         // Mapeamento dos atributos:
    //         Numero = conta.Number,
    //         Titular = conta.Holder,
    //         Saldo = conta.Balance,
    //         Tipo = conta.Type.ToString(),         // Converte o enum para string
    //         DataCriacao = conta.CreatedAt,
    //         Situacao = conta.Status.ToString()    // Converte o enum para string
    //     };


    // }


}