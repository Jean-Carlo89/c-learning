public class Program
{
    public static void Main(string[] args)
    {
        ContaCorrente conta = new ContaCorrente("12345-6", "João Silva", 1000.50m);

        System.Console.WriteLine("Número da Conta: " + conta.Numero);
        System.Console.WriteLine("Titular: " + conta.Titular);
        System.Console.WriteLine("Saldo: " + conta.ObterSaldo());

        conta.Depositar(500);
        conta.Sacar(200);

        System.Console.WriteLine("Saldo: " + conta.ObterSaldo());

        conta.Sacar(1400);

        System.Console.WriteLine("Saldo: " + conta.ObterSaldo());





        ContaCorrente conta3 = new ContaCorrente();

        System.Console.WriteLine("Número da Conta: " + conta3.Numero);
        System.Console.WriteLine("Titular: " + conta3.Titular);
        System.Console.WriteLine("Saldo: " + conta3.ObterSaldo());
    }
}