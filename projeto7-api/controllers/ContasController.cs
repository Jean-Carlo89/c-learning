using Microsoft.AspNetCore.Mvc;

namespace projeto7_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly List<Account> _accounts;
        public AccountsController(List<Account> accounts)
        {
            _accounts = accounts;
        }
        [HttpGet]
        public IActionResult GetContas()
        {


            var contasDTO = this._accounts
              .Select(conta => new AccountOutputDto
              {
                  Numero = conta.Numero,
                  Saldo = conta.Saldo,
                  Titular = conta.Titular,
                  Tipo = conta.Tipo

              })
              .ToList();


            return Ok(contasDTO);

        }

        [HttpGet("{numero}")]
        public IActionResult GetContasByNumber(string numero)
        {
            var conta = this._accounts.FirstOrDefault(c => c.Numero.ToString() == numero);
            if (conta == null)
            {
                return NotFound();
            }

            var contaDTO = new AccountOutputDto
            {
                Numero = conta.Numero,
                Saldo = conta.Saldo,
                Titular = conta.Titular,
                Tipo = conta.Tipo



            };
            return Ok(contaDTO);
        }

        [HttpDelete("{numero}")]
        public IActionResult DeleteConta(string numero)
        {

            var conta = this._accounts.FirstOrDefault(c => c.Numero.ToString() == numero);


            if (conta == null)
            {
                return NotFound();
            }


            this._accounts.Remove(conta);


            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateConta([FromBody] AccountInputDto AccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Account newAccount = new Account(this._accounts.Count + 1, AccountDto.Saldo, AccountDto.Tipo, AccountDto.Titular);
            this._accounts.Add(newAccount);
            return CreatedAtAction(nameof(GetContasByNumber), new { newAccount.Numero }, AccountDto);
        }

        [HttpPut("{numero}/deposit")]
        public IActionResult Deposit(string numero, [FromBody] DepositInputDto depositeDto)
        {

            if (!ModelState.IsValid || depositeDto.Valor <= 0)
            {

                return BadRequest("O valor do depósito deve ser positivo.");
            }


            var conta = this._accounts.FirstOrDefault(c => c.Numero.ToString() == numero);
            if (conta == null)
            {
                return NotFound("Conta não encontrada.");
            }


            conta.Saldo += depositeDto.Valor;




            return Ok();
        }


    }
}