using System.Threading.Tasks;
using BankSystem.API.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace projeto7_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController(BankContext context) : ControllerBase
    {
        //** metodo antigo
        // private readonly List<Conta> _accounts;

        int _count = 0;
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

            var accountModel = await context.Accounts.Include(c => c.Client).FirstOrDefaultAsync(c => c.Number == numero);

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


            Random random = new Random();
            int maxExclusive = 1000000;
            int randomNumber = random.Next(0, maxExclusive);

            BankAccount newAccount = new BankAccount(AccountDto, randomNumber, 1);

            BankAccountModel accountModel = BankAccountModelMapper.ToModel(newAccount);


            await context.AddAsync(accountModel);


            await context.SaveChangesAsync();



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


            var conta = await context.Accounts.FirstOrDefaultAsync(c => c.Number == numero);

            if (conta == null)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }


            BankAccount accountEntity = BankAccountModelMapper.ToEntity(conta);




            accountEntity.Deposit(depositeDto.Valor);

            // conta = BankAccountModelMapper.ToModel(accountEntity);



            conta.Balance += depositeDto.Valor;


            await context.SaveChangesAsync();


            return Ok(conta);
        }

        [HttpDelete("{numero}")]
        public async Task<IActionResult> DeleteConta(int numero)
        {

            var accountToDelete = await context.Accounts.FirstOrDefaultAsync(c => c.Number == numero);


            if (accountToDelete == null)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }


            context.Accounts.Remove(accountToDelete);
            await context.SaveChangesAsync();


            return NoContent();
        }


    }
}