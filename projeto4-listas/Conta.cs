public class Conta
{
    public int Numero { get; set; }
    public decimal Saldo { get; set; }
    public string Titular { get; set; }

    public string Tipo { get; set; }

    public Conta(int Numero, decimal Saldo, string Tipo, string Titular)
    {
        this.Numero = Numero;
        this.Saldo = Saldo;
        this.Tipo = Tipo;
        this.Titular = Titular;
    }


}