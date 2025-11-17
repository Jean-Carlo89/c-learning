using BankSystem.API.Repositories;
using Moq;

namespace projeto9_tests;

public class AccountServiceTest
{
    private Mock<IAccountRepository> _accountRepository;
    private AccountService _accountService;

    public AccountServiceTest()
    {

        this._accountRepository = new Mock<IAccountRepository>();
        _accountService = new AccountService(_accountRepository.Object);

    }


    [Fact]
    public async Task GetAllAccountAsync_MustReturnList_WhenAccountsExist()
    {
        var accounts = new List<BankAccount> {

        new BankAccount(4, 5, 45, AccountType.Corrente, "pedro", DateTime.Now, AccountStatus.Active, 6),

        new BankAccount(4, 5, 67, AccountType.Poupança, "lala", DateTime.Now, AccountStatus.Closed, 8)

        };
        var mappedAccounts = BankAccountModelMapper.ToModel(accounts);

        _accountRepository.Setup(repo => repo.GetAllAccountsAsync()).ReturnsAsync(mappedAccounts);

        try
        {
            var result = await _accountService.GetAllAccountsAsync();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            this._accountRepository.Verify(r => r.GetAllAccountsAsync(), Times.Once());
        }
        catch (Exception ex)
        {

            Console.WriteLine($"ERRO CAPTURADO: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            throw;
        }

    }

}




