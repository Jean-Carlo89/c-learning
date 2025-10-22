
public class Program
{
    // Clientes iniciais obrigatórios
    private static readonly string[] InitialClients = { "Jean", "Mike", "Sint" };


    private static IMockDatabase database;
    private static ITicketFactory ticketFactory;

    public static void Main(string[] args)
    {

        database = MockDatabase.GetInstance();
        ticketFactory = new TicketFactory();


        SetupInitialData(database);


        RunMenu();
    }


    private static void SetupInitialData(IMockDatabase database)
    {
        Console.WriteLine("\n--- SETUP INICIAL ---");
        foreach (var name in InitialClients)
        {


            database.AddClient(new Client(name));

        }

    }

    private static void RunMenu()
    {
        string choice;
        do
        {
            Console.WriteLine("\n====================================");
            Console.WriteLine("         MENU PRINCIPAL");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Listar Clientes Existentes");
            Console.WriteLine("2. Listar Ingressos de um Cliente");
            Console.WriteLine("3. Adicionar Ingresso a um Cliente");
            Console.WriteLine("4. Sair");
            Console.Write("Escolha uma opção: ");

            choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ListClients(database);
                    break;
                case "2":
                    ListClientTickets(database);
                    break;
                case "3":

                    AddTicketToClient(database, ticketFactory);
                    break;
                case "4":
                    Console.WriteLine("Encerrando o sistema...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

        } while (choice != "4");
    }

    // ==========================================================
    // OPÇÃO 1: Listar Clientes
    // ==========================================================
    private static void ListClients(IMockDatabase db)
    {
        Console.WriteLine("--- CLIENTES REGISTRADOS ---");
        var clients = db.ListClients();
        if (!clients.Any())
        {
            Console.WriteLine("Nenhum cliente no banco de dados.");
            return;
        }

        foreach (var client in clients)
        {
            Console.WriteLine($"- {client.Name} (Ingressos: {client.Tickets.Count})");
        }
    }
    private static void ListClientTickets(IMockDatabase database)
    {
        Console.Write("Digite o nome do cliente: ");
        string clientName = Console.ReadLine();

        Client client = database.GetClientByName(clientName);

        if (client == null)
        {
            Console.WriteLine($"Erro: Cliente '{clientName}' não encontrado.");
            return;
        }

        Console.WriteLine($"\n--- INGRESSOS DE {client.Name.ToUpper()} ({client.Tickets.Count} Total) ---");

        if (!client.Tickets.Any())
        {
            Console.WriteLine("O cliente não possui ingressos.");
            return;
        }

        foreach (var ticket in client.Tickets)
        {
            Console.WriteLine($"- {ticket.GetInfo()}");
        }
    }


    private static void AddTicketToClient(IMockDatabase db, ITicketFactory factory)
    {
        Console.Write("Digite o nome do cliente para adicionar o ingresso: ");
        string clientName = Console.ReadLine();


        Client client = db.GetClientByName(clientName);
        if (client == null)
        {
            Console.WriteLine($"Erro: Cliente '{clientName}' não encontrado.");
            return;
        }

        Console.WriteLine("\nTipos de Ingresso Disponíveis:");
        Console.WriteLine("  [1] VIP (Alto valor)");
        Console.WriteLine("  [2] REGULAR / PISTA (Padrão)");
        Console.Write("Escolha o tipo (1 ou 2): ");
        string typeChoice = Console.ReadLine();

        string ticketType;
        if (typeChoice == "1")
            ticketType = "VIP";
        else if (typeChoice == "2")
            ticketType = "standard";
        else
        {
            Console.WriteLine("Tipo inválido. Cancelando a compra.");
            return;
        }



        Console.Write($"\n{client.Name} comprando {ticketType.ToUpper()}... ");

        try
        {

            ITicket ticket = factory.Create(ticketType);


            client.AddTicket(ticket);
            db.AddTicket(ticket);

            Console.WriteLine($"Ingresso **{ticket.Description}** adicionado a **{client.Name}**.");
        }
        catch (ArgumentException ex)
        {

            Console.WriteLine($"Falha na Criação: {ex.Message}");
        }
    }
}