using BankSystem.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class ClientController(BankContext context) : ControllerBase
{


    [HttpPost]

    public async Task<IActionResult> CreateClient([FromBody] ClientInputDto ClientDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Client newClient = new Client(ClientDto.Nome, ClientDto.email);
        ClientModel newClientModel = ClientModelMapper.ToModel(newClient);

        await context.AddAsync(newClientModel);


        await context.SaveChangesAsync();



        return Ok();

    }

    // Rota: GET api/Client/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientOutputDto>> GetClient(int id)
    {

        var clientModel = await context.Clients.Include(c => c.Accounts).SingleOrDefaultAsync(c => c.Id == id);

        if (clientModel == null)
        {

            return NotFound($"Cliente com Id {id} não encontrado.");
        }


        var clientEntity = ClientModelMapper.ToEntity(clientModel);


        var clientDto = ClientModelMapper.ToOutputDto(clientEntity);


        return Ok(clientDto);
    }


}