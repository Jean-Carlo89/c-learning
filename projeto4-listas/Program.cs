
using System.ComponentModel;
using System.Linq;
public class Program
{
    public static void Main(string[] args)
    {

        List<Conta> contas = new List<Conta>();

        List<string> nomes = new List<string>() { "Ana", "Bruno", "Carlos", "Diana", "Eduardo" };

        for (int i = 0; i < 5; i++)
        {

            string tipo = i % 2 == 0 ? "Comum" : "Especial";
            Conta conta = new Conta(1000 + i, 500 * (i + 1), tipo, nomes[i]);
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



        Console.WriteLine("---- Dictionary ----");

        Dictionary<int, Conta> accountsDictionary = new Dictionary<int, Conta>();

        contas.ForEach((conta) => { accountsDictionary[conta.Numero] = conta; });

        foreach (var keyPair in accountsDictionary)
        {

            int numeroConta = keyPair.Key;


            Conta conta = keyPair.Value;


            Console.WriteLine($"Key:  {numeroConta}, Conta:{{ Saldo: {conta.Saldo}, Titular: {conta.Titular} }}");
        }

        Console.WriteLine("==============================================");
        Console.WriteLine("---- ATividades  LinQ----");
        Console.WriteLine("==============================================");
        Conta contaTeste = accountsDictionary[contas[0].Numero];
        contaTeste.Saldo = -25;
        accountsDictionary[contas[0].Numero] = contaTeste;


        int id = contas[0].Numero;


        var resultado = accountsDictionary

            .Where(par => par.Key == id)


            .Where(par => par.Value.Saldo < 0)


            .Select(par => par.Value)

            .FirstOrDefault();




        if (resultado != null)
        {
            Console.WriteLine($"Conta Negativa Encontrada - Número: {resultado.Numero}, Saldo: {resultado.Saldo}");
        }
        else
        {
            Console.WriteLine($"Nenhuma conta com a chave {id} e saldo negativo foi encontrada.");
        }


        var resultado2 = from par in accountsDictionary
                         where par.Key == id
                         where par.Value.Saldo < 0m
                         select par.Value;



        if (resultado2.ToList().Count > 0)
        {



            foreach (var conta in resultado2)
            {

                Console.WriteLine($"Número: {conta.Numero}, Titular: {conta.Titular}, Saldo: {conta.Saldo:C}");
            }
        }
        else
        {
            Console.WriteLine($"Nenhuma conta com o ID {id} e saldo negativo foi encontrada.");
        }



        Console.WriteLine("==============================================");
        Console.WriteLine("    Atividade 1 Contas por Tipo");
        Console.WriteLine("==============================================");

        var contasAgrupadas = from par in accountsDictionary.Values

                              group par by par.Tipo into grupo


                              select grupo;




        foreach (var group in contasAgrupadas)
        {

            Console.WriteLine($"\n--- TIPO DE CONTA: {group.Key} ---");
            Console.WriteLine($"Total de Contas neste grupo: {group.Count()}");


            foreach (var account in group)
            {

                Console.WriteLine($"  -> Número: {account.Numero}, Titular: {account.Titular}, Saldo: {account.Saldo:C}");
            }
        }

        Console.WriteLine("\n==============================================");
        Console.WriteLine("==============================================");
        Console.WriteLine("     parte 2 : Contas por Saldo");
        Console.WriteLine("==============================================");

        List<Conta> contasTotal = accountsDictionary.Values.ToList();

        var contasOrdenadas = contasTotal
            .OrderByDescending(c => c.Saldo);
        Console.WriteLine("--- Contas Ordenadas ---");
        foreach (var conta in contasOrdenadas)
        {
            Console.WriteLine($"Saldo: {conta.Saldo:C}, Número: {conta.Numero}, Titular: {conta.Titular}");
        }

        Console.WriteLine("\n==============================================");
        Console.WriteLine("==============================================");
        Console.WriteLine("     parte 3 : tipo anonmimo com select");
        Console.WriteLine("==============================================");

        var projecao = from par in accountsDictionary
                       select new
                       {


                           Saldo = par.Value.Saldo,
                           NomeDoTitular = par.Value.Titular
                       };

        Console.WriteLine("--- Projeção: Saldo e Titular ---");


        foreach (var item in projecao)
        {

            Console.WriteLine($"Titular: {item.NomeDoTitular}, Saldo Atual: {item.Saldo:C}");


        }


        Console.WriteLine("==============================================");
        Console.WriteLine("     parte 4 : juntando tudo");
        Console.WriteLine("==============================================");

        var groupedResult = from account in accountsDictionary.Values
                            group account by account.Tipo into g
                            select new
                            {
                                Tipo = g.Key,
                                Quantidade = g.Count(),
                                Contas = g.OrderByDescending(c => c.Saldo)
                                           .Select(c => new
                                           {
                                               Titular = c.Titular,
                                               Saldo = c.Saldo
                                           })
                                           .ToList()
                            };


        Console.WriteLine("=================================================");
        Console.WriteLine("  Atividade - Agrupando todas as atividades");
        Console.WriteLine("=================================================");

        foreach (var group in groupedResult)
        {
            Console.WriteLine($"\nTipo: {group.Tipo}");
            Console.WriteLine($" -> Total de Conta5s: {group.Quantidade}");

            Console.WriteLine(" Contas Ordenadas:");


            foreach (var contaProjetada in group.Contas)
            {
                Console.WriteLine($" - Titular: {contaProjetada.Titular}, Saldo: {contaProjetada.Saldo:C}");
            }
        }
        Console.WriteLine("=================================================");

    }
}