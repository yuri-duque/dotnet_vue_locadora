using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [StringLength(11, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 11)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public DateTime DataNascimento { get; set; }
    }
}
