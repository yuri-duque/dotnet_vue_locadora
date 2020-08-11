using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Locacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataLocacao { get; set; }

        public DateTime DataDevolucao { get; set; }

        #region Relacionamento

        [Required]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        #endregion
    }
}
