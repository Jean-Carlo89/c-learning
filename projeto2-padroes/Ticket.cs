
public class RegularTicket : ITicket
{
    public string Id { get; private set; }
    public decimal Price { get; }

    public string Description => "Imngresso comum";



    public RegularTicket()
    {

        Id = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        this.Price = 100;
    }

    public string GetInfo()
    {
        return $"Ingresso ID: {Id}, Tipo: {Description}, Preço: R$ {Price}";
    }



}