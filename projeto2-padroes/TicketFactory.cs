
public class TicketFactory : ITicketFactory
{

    public ITicket Create(string type)
    {


        return type.ToLower() switch
        {
            "vip" => new VipTicket(),
            "standard" => new RegularTicket(),


            _ => throw new ArgumentException($"unknown ticket type: {type}"),
        };
    }
}