using BankSystem.API.model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankSystem.API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;


        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<BankAccountModel> GetAccountByNumberAsync(int numero)
        {

            return await _context.Accounts
                                 .Include(c => c.Client)
                                 .FirstOrDefaultAsync(c => c.Number == numero);
        }

        public async Task<bool> checkIfClientExistsByIdAsync(int clientId)
        {
            return await _context.Clients.AnyAsync(c => c.Id == clientId);
        }

        public async Task AddNewAccountAsync(BankAccountModel account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public Task UpdateAccountAsync(BankAccountModel account)
        {

            _context.Accounts.Update(account);
            return Task.CompletedTask;
        }

        public Task DeleteAccountAsync(BankAccountModel account)
        {
            _context.Accounts.Remove(account);
            return Task.CompletedTask;
        }

        public async Task SaveDatabaseChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}