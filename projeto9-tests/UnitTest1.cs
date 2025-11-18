// using BankSystem.API.model;
// using BankSystem.API.Repositories;
// using Microsoft.EntityFrameworkCore.Metadata.Internal;
// using Moq;

// namespace projeto9_tests;

// public class AccountServiceTest
// {
//     private Mock<IAccountRepository> _accountRepository;
//     private AccountService _accountService;

//     public AccountServiceTest()
//     {

//         this._accountRepository = new Mock<IAccountRepository>();
//         _accountService = new AccountService(_accountRepository.Object);

//     }


//     [Fact]
//     public async Task GetAllAccountAsync_MustReturnList_WhenAccountsExist()
//     {
//         var accounts = new List<BankAccount> {

//         new BankAccount(4, 5, 45, AccountType.Corrente, "pedro", DateTime.Now, AccountStatus.Active, 6),

//         new BankAccount(4, 5, 67, AccountType.Poupança, "lala", DateTime.Now, AccountStatus.Closed, 8)

//         };
//         var mappedAccounts = BankAccountModelMapper.ToModel(accounts);

//         _accountRepository.Setup(repo => repo.GetAllAccountsAsync()).ReturnsAsync(mappedAccounts);

//         try
//         {
//             var result = await _accountService.GetAllAccountsAsync();
//             Assert.NotNull(result);
//             Assert.Equal(2, result.Count);
//             this._accountRepository.Verify(r => r.GetAllAccountsAsync(), Times.Once());
//         }
//         catch (Exception ex)
//         {

//             Console.WriteLine($"ERRO CAPTURADO: {ex.Message}");
//             Console.WriteLine($"Stack Trace: {ex.StackTrace}");

//             throw;
//         }

//     }

//     [Fact]
//     public async Task GetAllAccountsAsync_ShouldReturnEmptyList_WhenNoAccountExistis()
//     {
//         var accounts = new List<BankAccountModel>();

//         _accountRepository.Setup(repo => repo.GetAllAccountsAsync()).ReturnsAsync(accounts);


//         try
//         {
//             var result = await _accountService.GetAllAccountsAsync();
//             Assert.Empty(result);
//             this._accountRepository.Verify(r => r.GetAllAccountsAsync(), Times.Once());

//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"ERRO CAPTURADO: {ex.Message}");
//             Console.WriteLine($"Stack Trace: {ex.StackTrace}");

//             throw;
//         }

//     }

//     [Fact]
//     public async Task GetAllAccountsAsync_ShouldReturnException_WhenObjectIsNull()
//     {

//         _accountRepository
//            .Setup(repo => repo.GetAllAccountsAsync())
//            .ThrowsAsync(new ArgumentNullException("repository"));


//         await Assert.ThrowsAsync<ArgumentNullException>(() => _accountService.GetAllAccountsAsync());
//     }

//     [Fact]
//     public async Task GetAccountAsync_ShouldReturnAccount_WhenAccountExists()
//     {



//         var account = new BankAccount(4, 5, 45, AccountType.Corrente, "pedro", DateTime.Now, AccountStatus.Active, 6);

//         var accountNumber = 5;
//         var balance = 45m;

//         var mappedAccount = BankAccountModelMapper.ToModel(account);

//         _accountRepository.Setup(repo => repo.GetAccountByNumberAsync(accountNumber)).ReturnsAsync(mappedAccount);

//         try
//         {


//             var result = await _accountService.GetAccountByNumberAsync(accountNumber);
//             Assert.NotNull(result);

//             Assert.Equal(accountNumber, result.Numero);
//             Assert.Equal(balance, result.Saldo);
//             this._accountRepository.Verify(r => r.GetAccountByNumberAsync(accountNumber), Times.Once());
//         }
//         catch (Exception ex)
//         {

//             Console.WriteLine($"ERRO CAPTURADO: {ex.Message}");
//             Console.WriteLine($"Stack Trace: {ex.StackTrace}");

//             throw;
//         }

//     }


//     //*** Client


//     [Fact]
//     public async Task DeleteAccountAsync_ShouldDeleteAccount_WhenAccountExists()
//     {

//         var accountNumber = 123;
//         var account = new BankAccount(accountNumber, 5, 45, AccountType.Corrente, "pedro", DateTime.Now, AccountStatus.Active, 6);

//         var model = BankAccountModelMapper.ToModel(account);

//         _accountRepository
//           .Setup(repo => repo.GetAccountByNumberAsync(accountNumber)).ReturnsAsync(model);




//         await _accountService.deleteAccountAsync(accountNumber);

//         _accountRepository.Verify(repo => repo.DeleteAccountAsync(model), Times.Once());
//     }


// }




