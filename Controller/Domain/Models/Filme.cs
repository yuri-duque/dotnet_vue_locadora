using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }

        [Key, Required, MaxLength(100)]
        public string Titulo { get; set; }

        [Required]
        public EnumClassificacaoIndicativa ClassificacaoIndicativa { get; set; }

        [Required]
        public bool Lancamento { get; set; } // false = Comum, true = Lancamento

        #region Relacionamento

        public IList<Locacao> Locacoes { get; set; }

        #endregion

        #region Mapeamento

        public static void Map(ModelBuilder modelBuilder)
        {
            var map = modelBuilder.Entity<Filme>();
            map.HasKey(x => new { x.Id, x.Titulo });
            map.Property(x => x.Id).ValueGeneratedOnAdd();
        }

        #endregion
    }
}
