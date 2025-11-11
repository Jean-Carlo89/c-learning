using BankSystem.API.model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BankSystem.API.Repositories
{
    public interface IAccountRepository
    {
        Task<BankAccountModel> GetByNumberAsync(int numero);
        Task<bool> ClientExistsAsync(int clientId);
        Task AddAsync(BankAccountModel account);
        Task UpdateAsync(BankAccountModel account);
        Task DeleteAsync(BankAccountModel account);
        Task SaveChangesAsync();
    }
}