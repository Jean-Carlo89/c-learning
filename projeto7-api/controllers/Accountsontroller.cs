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

        private readonly IAccountService _accountService;


        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;

        }


        [HttpGet("{numero}")]
        public async Task<IActionResult> GetContasByNumber(int numero)
        {
            var account = await this._accountService.GetAccountByNumberAsync(numero);


            if (account == null)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }


            return Ok(account);
        }



        [HttpPost]
        public async Task<IActionResult> CreateConta([FromBody] AccountInputDto AccountDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientExists = await this._accountService.checkIfClientExistsByIdAsync(AccountDto.ClientId);
            if (!clientExists)
            {
                return NotFound($"Cliente com Id {AccountDto.ClientId} não encontrado.");
            }

            await this._accountService.AddNewAccountAsync(AccountDto);

            return Ok();
        }


        [HttpPut("{numero}/deposit")]
        public async Task<IActionResult> Deposit(int numero, [FromBody] DepositInputDto depositeDto)
        {

            if (!ModelState.IsValid || depositeDto.Valor <= 0)
            {

                return BadRequest("O valor do depósito deve ser positivo.");
            }

            var accountExists = await this._accountService.checkIfAccountExistsByNumber(numero);

            if (accountExists == false)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }

            await this._accountService.depositInAccount(numero, depositeDto.Valor);

            return Ok();
        }

        [HttpPut("{numero}/withdraw")]
        public async Task<IActionResult> Withdraw(int numero, [FromBody] WithdrawInputDto withdrawDto)
        {


            var accountExists = await this._accountService.checkIfAccountExistsByNumber(numero);

            if (accountExists == false)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }

            await this._accountService.withdrawFromAccount(numero, withdrawDto.Valor);

            return Ok();
        }

        [HttpDelete("{numero}")]
        public async Task<IActionResult> DeleteConta(int numero)
        {
            var accountsExists = await this._accountService.checkIfAccountExistsByNumber(numero);

            if (accountsExists == false)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }

            await this._accountService.deleteAccount(numero);

            return NoContent();
        }


    }
}