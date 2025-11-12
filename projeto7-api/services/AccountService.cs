using BankSystem.API.model;
using BankSystem.API.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

public class AccountService : IAccountService
{

    private readonly IAccountRepository repository;
    public AccountService(IAccountRepository repository)
    {

        this.repository = repository;
    }

    public async Task AddNewAccountAsync(AccountInputDto accountDto)
    {

        Random random = new Random();
        int maxExclusive = 1000000;
        int randomNumber = random.Next(0, maxExclusive);

        BankAccount newAccount = new BankAccount(accountDto, randomNumber, accountDto.ClientId);

        BankAccountModel accountModel = BankAccountModelMapper.ToModel(newAccount);

        await this.repository.AddNewAccountAsync(accountModel);
        await this.repository.SaveDatabaseChangesAsync();
    }

    public async Task<bool> checkIfAccountExistsByNumber(int accountNumber)
    {
        BankAccountModel account = await this.repository.GetAccountByNumberAsync(accountNumber);
        if (account == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> checkIfClientExistsByIdAsync(int clientId)
    {
        return await this.repository.checkIfClientExistsByIdAsync(clientId);
    }



    public async Task depositInAccount(int accountNumber, decimal amount)
    {

        BankAccountModel account = await this.repository.GetAccountByNumberAsync(accountNumber);

        // if (account == null)
        // {
        //     return false;
        // }
        BankAccount accountEntity = BankAccountModelMapper.ToEntity(account);


        accountEntity.Deposit(amount);


        account.Balance = accountEntity.Balance;


        await this.repository.SaveDatabaseChangesAsync();
    }




    public async Task<AccountOutputDto> GetAccountByNumberAsync(int accountNumber)
    {
        var accountModel = await this.repository.GetAccountByNumberAsync(accountNumber);

        if (accountModel == null)
        {
            return null;

        }
        BankAccount accountEntity = BankAccountModelMapper.ToEntity(accountModel);


        AccountOutputDto account = BankAccountModelMapper.ToOutputDto(accountEntity);

        return account;
    }

    public async Task withdrawFromAccount(int accountNumber, decimal amount)
    {
        BankAccountModel account = await this.repository.GetAccountByNumberAsync(accountNumber);


        BankAccount accountEntity = BankAccountModelMapper.ToEntity(account);


        accountEntity.Withdraw(amount);


        account.Balance = accountEntity.Balance;

        await this.repository.SaveDatabaseChangesAsync();
    }

    public async Task deleteAccount(int accountNumber)
    {

        //*****  Atualizar métodod de delete para fazer direto pelo Id da conta


        BankAccountModel account = await this.repository.GetAccountByNumberAsync(accountNumber);

        await this.repository.DeleteAccountAsync(account);
        await this.repository.SaveDatabaseChangesAsync();

    }
}

