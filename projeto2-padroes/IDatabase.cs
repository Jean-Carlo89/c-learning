

public interface IMockDatabase
{

    void AddClient(Client client);
    void AddTicket(ITicket ticket);

    List<Client> ListClients();

    List<ITicket> ListAllTickets();

    Client GetClientByName(string clientName);





}