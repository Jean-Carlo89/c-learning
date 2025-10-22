public class ContaEspecial : Conta
{
    public decimal Limite { get; set; }

    public ContaEspecial(string numero, string titular, decimal saldo, decimal limiteEspecial)
        : base(numero, titular, saldo)
    {
        Limite = limiteEspecial;
    }

    public ContaEspecial()
            : base()
    {
        Limite = 0;
    }



}