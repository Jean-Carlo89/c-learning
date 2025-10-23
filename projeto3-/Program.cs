public class Program
{
    public static void Main(string[] args)
    {
        Conta conta = new Conta("12345-6", "João Silva", 1000.50m);

        System.Console.WriteLine("Número da Conta: " + conta.Numero);
        System.Console.WriteLine("Titular: " + conta.Titular);
        System.Console.WriteLine("Saldo: " + conta.ObterSaldo());

        conta.Depositar(500);
        conta.Sacar(200);

        System.Console.WriteLine("Saldo: " + conta.ObterSaldo());

        conta.Sacar(1400);

        System.Console.WriteLine("Saldo: " + conta.ObterSaldo());

        System.Console.WriteLine("tarifa: " + conta.CalcularTarifa());





        Conta conta2 = new Conta();

        System.Console.WriteLine("Número da Conta: " + conta2.Numero);
        System.Console.WriteLine("Titular: " + conta2.Titular);
        System.Console.WriteLine("Saldo: " + conta2.ObterSaldo());

        System.Console.WriteLine("tarifa: " + conta2.CalcularTarifa());


        Conta contaEspecial = new ContaEspecial("98765-4", "Maria Oliveira", 2000.00m, 500.00m);

        System.Console.WriteLine("Número da Conta Especial: " + contaEspecial.Numero);
        System.Console.WriteLine("Titular: " + contaEspecial.Titular);
        System.Console.WriteLine("Saldo: " + contaEspecial.ObterSaldo());

        contaEspecial.Depositar(300);
        contaEspecial.Sacar(2500);

        System.Console.WriteLine("Saldo: " + contaEspecial.ObterSaldo());

        System.Console.WriteLine("tarifa: " + contaEspecial.CalcularTarifa());

        Conta contaPoupanca = new ContaPoupanca("54321-0", "Carlos Pereira", 1500.00m, 0.05m);

        System.Console.WriteLine("Número da Conta Poupança: " + contaPoupanca.Numero);
        System.Console.WriteLine("Titular: " + contaPoupanca.Titular);
        System.Console.WriteLine("Saldo: " + contaPoupanca.ObterSaldo());

        contaPoupanca.Depositar(400);
        contaPoupanca.Sacar(1000);

        System.Console.WriteLine("Saldo: " + contaPoupanca.ObterSaldo());
        System.Console.WriteLine("tarifa: " + contaPoupanca.CalcularTarifa());



    }
}