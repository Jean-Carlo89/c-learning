// VipTicket.cs
public class VipTicket : ITicket
{
    public string Id { get; }
    public decimal Price { get; }
    public string Description => "VIP com acesso exclusiv0";

    public string code { get; }

    public VipTicket()
    {
        Id = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        this.Price = 350;
        this.code = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }

    public string GetInfo()
    {
        return $"Ingresso ID: {Id}, Tipo: {Description}, Preço: R$ {Price}, Code: {code}";
    }
}