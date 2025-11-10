
public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    public List<BankAccount> Accounts { get; set; }

    public Client(int Id, string Name, string Email, DateTime? DateOfBirth = null)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.DateOfBirth = DateOfBirth ?? DateTime.Now;
    }

    public Client(string Name, string Email, DateTime? DateOfBirth = null)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.DateOfBirth = DateOfBirth ?? DateTime.Now;
    }

    public Client(int Id, string Name, string Email)
    {

        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        // this.DateOfBirth = DateOfBirth;
    }
}