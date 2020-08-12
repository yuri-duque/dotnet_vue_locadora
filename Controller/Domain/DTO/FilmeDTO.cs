using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class FilmeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public EnumClassificacaoIndicativa ClassificacaoIndicativa { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public bool Lancamento { get; set; } // false = Comum, true = Lancamento
    }
}
