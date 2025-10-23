public class ContaPoupanca : Conta
{
    public decimal TaxaJuros { get; set; }

    public ContaPoupanca(string numero, string titular, decimal saldo, decimal taxaJuros)
        : base(numero, titular, saldo)
    {
        TaxaJuros = taxaJuros;
    }

    public void AplicarJuros()
    {
        decimal juros = ObterSaldo() * TaxaJuros;
        Depositar(juros);
    }

    public override decimal CalcularTarifa()
    {
        return 2.00m;
    }
}