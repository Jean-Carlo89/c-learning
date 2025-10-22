
public sealed class MockDatabase : IMockDatabase
{

    private MockDatabase()
    {

        clients = new List<Client>();
        tickets = new List<ITicket>();

    }


    private static MockDatabase? instance;


    public static MockDatabase GetInstance()
    {

        if (instance == null)
        {
            instance = new MockDatabase();
        }
        return instance;
    }


    private readonly List<Client> clients;
    private readonly List<ITicket> tickets;



    public void AddClient(Client client)
    {
        if (GetClientByName(client.Name) == null)
        {
            clients.Add(client);
        }
    }

    public void AddTicket(ITicket ticket)
    {
        tickets.Add(ticket);
    }

    public List<Client> ListClients() => this.clients;

    public List<ITicket> ListAllTickets() => this.tickets;

    public Client GetClientByName(string clientName) =>
        clients.FirstOrDefault(c => c.Name.Equals(clientName, StringComparison.OrdinalIgnoreCase));
}