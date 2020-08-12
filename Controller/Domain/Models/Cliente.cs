using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(11), MinLength(11)]
        public string CPF { get; set; }

        [Required, MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        #region Relacionamento

        public IList<Locacao> Locacoes { get; set; }

        #endregion

        #region Mapeamento

        public static void Map(ModelBuilder modelBuilder)
        {
            var map = modelBuilder.Entity<Cliente>();
            map.Property(x => x.Id).ValueGeneratedOnAdd();

            map.HasIndex(x => x.CPF).IsUnique();
        }

        #endregion
    }
}
