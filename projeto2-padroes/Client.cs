

public class Client
{
    public string Name { get; }

    public List<ITicket> Tickets;

    public Client(string name)
    {
        Name = name;
        Tickets = new List<ITicket>();
    }


    public void AddTicket(ITicket ticket)
    {
        Tickets.Add(ticket);
    }

    public List<ITicket> listTickets()
    {
        return this.Tickets;
    }



}