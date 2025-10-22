public class ContaCorrente
{
    public string Numero { get; set; }
    public string Titular { get; set; }

    private decimal Saldo;

    public ContaCorrente()
    {
        this.Numero = "sem numeracao";
        this.Titular = "sem titular";
        this.Saldo = 0;
    }

    public ContaCorrente(string numero, string titular, decimal saldo)
    {

        this.Numero = numero;
        this.Titular = titular;
        this.Saldo = saldo;
    }

    public Boolean Depositar(decimal valor)
    {
        if (valor > 0)
        {
            this.Saldo += valor;
            return true;
        }
        else
        {
            return false;
        }


    }

    public Boolean Sacar(decimal valor)
    {
        if (valor > 0 && this.Saldo >= valor)
        {
            this.Saldo -= valor;
            return true;
        }

        return false;
    }

    public decimal ObterSaldo()
    {
        return this.Saldo;
    }

}
