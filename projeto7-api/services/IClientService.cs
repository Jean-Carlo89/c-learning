using System.Threading.Tasks;
using BankSystem.API.Dtos;

public interface IClientService
{

    Task<ClientOutputDto> GetClientByIdAsync(int clientId);

    Task<ClientOutputDto> AddNewClientAsync(ClientInputDto client);


}