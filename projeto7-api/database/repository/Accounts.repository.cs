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

        public async Task<BankAccountModel> GetByNumberAsync(int numero)
        {

            return await _context.Accounts
                                 .Include(c => c.Client)
                                 .FirstOrDefaultAsync(c => c.Number == numero);
        }

        public async Task<bool> ClientExistsAsync(int clientId)
        {
            return await _context.Clients.AnyAsync(c => c.Id == clientId);
        }

        public async Task AddAsync(BankAccountModel account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public Task UpdateAsync(BankAccountModel account)
        {

            _context.Accounts.Update(account);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(BankAccountModel account)
        {
            _context.Accounts.Remove(account);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}