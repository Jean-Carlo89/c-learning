
using System.ComponentModel;

public class Program
{
    public static void Main(string[] args)
    {

        List<Conta> contas = new List<Conta>();

        List<string> nomes = new List<string>() { "Ana", "Bruno", "Carlos", "Diana", "Eduardo" };

        for (int i = 0; i < 5; i++)
        {
            Conta conta = new Conta(1000 + i, 500 * (i + 1), "Corrente", nomes[i]);
            contas.Add(conta);
        }

        foreach (var conta in contas)
        {
            Console.WriteLine($"Conta: Número: {conta.Numero}, Saldo: {conta.Saldo}, Tipo: {conta.Tipo}, Titular: {conta.Titular}");
        }

        contas.RemoveAll(c => c.Numero == 1001);

        Console.WriteLine(contas.Count());


        Console.WriteLine(contas.Find(c => c.Titular == "Diana").Saldo);

        Console.WriteLine("---- Contas Fixas ----");


        Conta[] contasFixas = new Conta[4];

        for (int i = 0; i < 4; i++)
        {
            Conta conta = new Conta(1000 + i, 500 * (i + 1), "Corrente", nomes[i]);
            contasFixas[i] = conta;
        }


        foreach (var conta in contasFixas)
        {
            Console.WriteLine($"Conta: Número: {conta.Numero}, Saldo: {conta.Saldo}, Tipo: {conta.Tipo}, Titular: {conta.Titular}");
        }

        contasFixas[5] = new Conta(2000, 3000, "Poupança", "Fernanda");
    }
}