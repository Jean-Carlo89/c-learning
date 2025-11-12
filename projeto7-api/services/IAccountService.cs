using System.Threading.Tasks;

public interface IAccountService
{

    Task<AccountOutputDto> GetAccountByNumberAsync(int accountNumber);

    Task AddNewAccountAsync(AccountInputDto account);

    Task<bool> checkIfClientExistsByIdAsync(int clientId);

    Task depositInAccount(int accountNumber, decimal amount);
    Task withdrawFromAccount(int accountNumber, decimal amount);

    Task<bool> checkIfAccountExistsByNumber(int accountNumber);

    Task deleteAccount(int accountNumber);
}