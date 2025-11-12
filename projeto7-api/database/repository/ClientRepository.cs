using BankSystem.API.model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankSystem.API.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly BankContext _context;


        public ClientRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<ClientModel> GetClientByIdAsync(int Id)
        {

            return await _context.Clients
                                 .Include(c => c.Accounts)
                                 .FirstOrDefaultAsync(c => c.Id == Id);
        }



        public async Task AddNewClientAsync(ClientModel client)
        {
            await _context.Clients.AddAsync(client);
        }

        // public Task UpdateAsync(BankAccountModel account)
        // {

        //     _context.Accounts.Update(account);
        //     return Task.CompletedTask;
        // }

        // public Task DeleteAsync(BankAccountModel account)
        // {
        //     _context.Accounts.Remove(account);
        //     return Task.CompletedTask;
        // }

        public async Task SaveDatabaseChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}