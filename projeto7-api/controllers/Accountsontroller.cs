using System.Threading.Tasks;
using BankSystem.API.model;
using BankSystem.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace projeto7_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {

        //(BankContext context)
        //** metodo antigo
        // private readonly List<Conta> _accounts;
        private readonly IAccountRepository _repository;
        int _count = 0;

        public AccountsController(IAccountRepository repository)
        {
            _repository = repository;
        }
        // private BankContext _context;
        // public AccountsController(List<Account> accounts, BankContext context)
        // {
        //     _accounts = accounts;
        //     // _context = context;
        // }
        // [HttpGet]
        // public IActionResult GetContas()
        // {


        //     var contasDTO = this._accounts
        //       .Select(conta => new AccountOutputDto
        //       {
        //           Numero = conta.Numero,
        //           Saldo = conta.Saldo,
        //           Titular = conta.Titular,
        //           Tipo = conta.Tipo

        //       })
        //       .ToList();


        //     return Ok(contasDTO);

        // }

        [HttpGet("{numero}")]
        public async Task<IActionResult> GetContasByNumber(int numero)
        {

            // var accountModel = await context.Accounts.Include(c => c.Client).FirstOrDefaultAsync(c => c.Number == numero);

            var accountModel = await this._repository.GetByNumberAsync(numero);


            BankAccount accountEntity = BankAccountModelMapper.ToEntity(accountModel);


            AccountOutputDto account = BankAccountModelMapper.ToOutputDto(accountEntity);

            if (account == null)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }


            return Ok(account);
        }



        [HttpPost]
        public async Task<IActionResult> CreateConta([FromBody] AccountInputDto AccountDto)
        {

            this._count = _count + 1;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            // var clientExists = await context.Clients.AnyAsync(c => c.Id == AccountDto.ClientId);

            var clientExists = await this._repository.ClientExistsAsync(AccountDto.ClientId);
            if (!clientExists)
            {
                return NotFound($"Cliente com Id {AccountDto.ClientId} não encontrado.");
            }


            Random random = new Random();
            int maxExclusive = 1000000;
            int randomNumber = random.Next(0, maxExclusive);

            BankAccount newAccount = new BankAccount(AccountDto, randomNumber, AccountDto.ClientId);

            BankAccountModel accountModel = BankAccountModelMapper.ToModel(newAccount);

            await this._repository.AddAsync(accountModel);
            await this._repository.SaveChangesAsync();
            // await context.AddAsync(accountModel);


            // await context.SaveChangesAsync();



            return CreatedAtAction(
                    nameof(GetContasByNumber),
                    new { numero = newAccount.Number },
                    AccountDto
                );
        }




        [HttpPut("{numero}/deposit")]
        public async Task<IActionResult> Deposit(int numero, [FromBody] DepositInputDto depositeDto)
        {

            if (!ModelState.IsValid || depositeDto.Valor <= 0)
            {

                return BadRequest("O valor do depósito deve ser positivo.");
            }


            // var conta = await context.Accounts.FirstOrDefaultAsync(c => c.Number == numero);
            BankAccountModel conta = await this._repository.GetByNumberAsync(numero);

            if (conta == null)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }


            BankAccount accountEntity = BankAccountModelMapper.ToEntity(conta);




            accountEntity.Deposit(depositeDto.Valor);

            // conta = BankAccountModelMapper.ToModel(accountEntity);



            conta.Balance = accountEntity.Balance;


            // await context.SaveChangesAsync();

            await this._repository.SaveChangesAsync();


            return Ok();
        }

        [HttpPut("{numero}/withdraw")]
        public async Task<IActionResult> Withdraw(int numero, [FromBody] WithdrawInputDto withdrawDto)
        {

            if (!ModelState.IsValid || withdrawDto.Valor <= 0)
            {
                return BadRequest("O valor do saque deve ser positivo.");
            }


            // var conta = await context.Accounts.FirstOrDefaultAsync(c => c.Number == numero);
            var conta = await this._repository.GetByNumberAsync(numero);

            if (conta == null)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }


            BankAccount accountEntity = BankAccountModelMapper.ToEntity(conta);

            try
            {

                accountEntity.Withdraw(withdrawDto.Valor);


                conta.Balance = accountEntity.Balance;


            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }


            // await context.SaveChangesAsync();
            await this._repository.SaveChangesAsync();


            return Ok();
        }

        [HttpDelete("{numero}")]
        public async Task<IActionResult> DeleteConta(int numero)
        {

            //  var accountToDelete = await context.Accounts.FirstOrDefaultAsync(c => c.Number == numero);

            var accountToDelete = await this._repository.GetByNumberAsync(numero);

            if (accountToDelete == null)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }


            // context.Accounts.Remove(accountToDelete);
            // await context.SaveChangesAsync();
            await this._repository.DeleteAsync(accountToDelete);
            await this._repository.SaveChangesAsync();


            return NoContent();
        }


    }
}