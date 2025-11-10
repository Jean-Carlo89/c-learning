using System.Collections.Generic;

namespace BankSystem.API.Dtos
{
    public class ClientOutputDto
    {

        public int Id { get; set; }

        // Nome completo
        public string Name { get; set; }


        public string Email { get; set; }

        public DateTime DOB { get; set; }


        public DateTime DateOfBirth { get; set; }

        public List<AccountOutputDto> Accounts { get; set; }


    }
}