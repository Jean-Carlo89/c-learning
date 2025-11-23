using System.Threading.Tasks;
using BankSystem.API.Repositories;
using BankSystem.API.model;
using BankSystem.Domain.Entities; // Para acessar a entidade de domínio BankAccount
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; // Para KeyNotFoundException

namespace BankSystem.API.Services
{
    // O IAccountService existente poderia ser substituído por IAccountRepository,
    // mas vamos manter o IAccountRepository e ITransactionRepository.
    public class TransferService : ITransferService
    {
        private readonly BankContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;


        public TransferService(
            BankContext context,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task ExecuteTransferAsync(int sourceAccountNumber, int destinationAccountNumber, decimal amount)
        {
            //** TRansactions
            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {

                BankAccountModel? sourceAccountModel = await _accountRepository.GetAccountByNumberAsync(sourceAccountNumber);
                BankAccountModel? destinationAccountModel = await _accountRepository.GetAccountByNumberAsync(destinationAccountNumber);

                if (sourceAccountModel == null)
                {
                    throw new KeyNotFoundException($"Conta de origem com número {sourceAccountNumber} não encontrada.");
                }
                if (destinationAccountModel == null)
                {
                    throw new KeyNotFoundException($"Conta de destino com número {destinationAccountNumber} não encontrada.");
                }



                BankAccount sourceAccountEntity = BankAccountModelMapper.ToEntity(sourceAccountModel);
                BankAccount destinationAccountEntity = BankAccountModelMapper.ToEntity(destinationAccountModel);

                sourceAccountEntity.Withdraw(amount);
                destinationAccountEntity.Deposit(amount);


                sourceAccountModel.Balance = sourceAccountEntity.Balance;
                destinationAccountModel.Balance = destinationAccountEntity.Balance;



                var transactionModel = new TransactionModel
                {
                    Type = TransactionType.Transfer,
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow,
                    SourceAccountId = sourceAccountModel.Id,
                    DestinationAccountId = destinationAccountModel.Id
                };

                await _transactionRepository.AddTransactionAsync(transactionModel);


                await _context.SaveChangesAsync();


                await dbTransaction.CommitAsync();
            }
            catch (Exception)
            {

                await dbTransaction.RollbackAsync();
                throw;
            }
        }
    }
}