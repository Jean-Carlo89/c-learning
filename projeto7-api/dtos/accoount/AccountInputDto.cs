
using System.ComponentModel.DataAnnotations;

public class AccountInputDto

{
    [Range(0, 1000000, ErrorMessage = "O saldo inicial deve ser positivo.")]
    public decimal Saldo { get; set; }

    [Required(ErrorMessage = "O nome do titular é obrigatório.")]
    public string Titular { get; set; }


    [Required(ErrorMessage = "O tipo de conta é obrigatório.")]
    public string Tipo { get; set; }

}
