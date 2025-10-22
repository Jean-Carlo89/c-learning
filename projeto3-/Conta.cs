public class Conta
{
    public string Numero { get; set; }
    public string Titular { get; set; }

    private decimal _saldo;

    public Conta()
    {
        this.Numero = "sem numeracao";
        this.Titular = "sem titular";
        this._saldo = 0;
    }

    public Conta(string numero, string titular, decimal saldo)
    {

        this.Numero = numero;
        this.Titular = titular;
        this._saldo = saldo;
    }

    public Boolean Depositar(decimal valor)
    {
        if (valor > 0)
        {
            this._saldo += valor;
            return true;
        }
        else
        {
            return false;
        }


    }

    public Boolean Sacar(decimal valor)
    {
        if (valor > 0 && this._saldo >= valor)
        {
            this._saldo -= valor;
            return true;
        }

        return false;
    }

    public decimal ObterSaldo()
    {
        return this._saldo;
    }

}
