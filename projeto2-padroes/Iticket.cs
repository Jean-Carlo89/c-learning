public interface ITicket
{
    string Id { get; }
    decimal Price { get; }
    string Description { get; }
    string GetInfo();
}