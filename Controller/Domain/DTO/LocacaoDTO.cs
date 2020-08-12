using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class LocacaoDTO
    {
        public int Id { get; set; }
        public ClienteDTO Cliente { get; set; }
        public FilmeDTO Filme { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool FilmeDevolvido { get; set; }
    }

    public class LocacaoAlugarDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public int IdFilme { get; set; }
    }

    public class LocacaoAtualizarDTO
    {
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public int IdFilme { get; set; }

        [Required(ErrorMessage = "A {0} é obrigatória!")]
        public DateTime DataLocacao { get; set; }

        [Required(ErrorMessage = "A {0} é obrigatória!")]
        public DateTime DataDevolucao { get; set; }

        public bool FilmeDevolvido { get; set; }
    }
}
