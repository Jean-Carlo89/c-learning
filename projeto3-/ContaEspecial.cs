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

    public override decimal CalcularTarifa()
    {

        const decimal tariffProportion = 0.01m;

        decimal tarifaCalculada = this.Limite * tariffProportion;


        return tarifaCalculada;
    }

    public override bool Sacar(decimal valor)
    {
        if (valor > 0 && (ObterSaldo() + Limite) >= valor)
        {
            decimal saldoAtual = ObterSaldo();
            if (saldoAtual >= valor)
            {
                // *** sacar normalmente
                return base.Sacar(valor);
            }
            else
            {
                // ** limite especial
                decimal valorDoLimite = valor - saldoAtual;
                base.Sacar(saldoAtual);
                Limite -= valorDoLimite; // Reduz o limite especial
                return true;
            }
        }

        return false;
    }
}