using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Locacao
    {
        [Required]
        public DateTime DataLocacao { get; set; }

        public DateTime? DataDevolucao { get; set; }

        #region Relacionamento

        [Required]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }


        [Required]
        public int IdFilme { get; set; }
        public Filme Filme { get; set; }

        #endregion

        #region Mapeamento

        public static void Map(ModelBuilder modelBuilder)
        {
            var map = modelBuilder.Entity<Locacao>();
            map.HasKey(x => new { x.IdCliente, x.IdFilme });

            map
                .HasOne(x => x.Cliente)
                .WithMany(y => y.Locacoes)
                .HasForeignKey(x => x.IdCliente)
                .HasPrincipalKey(y => y.Id)
                .OnDelete(DeleteBehavior.Cascade);

            map
                .HasOne(x => x.Filme)
                .WithMany(y => y.Locacoes)
                .HasForeignKey(x => x.IdFilme)
                .HasPrincipalKey(y => y.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }

        #endregion
    }
}
