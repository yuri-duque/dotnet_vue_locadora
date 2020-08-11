﻿using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        public short Lancamento { get; set; }

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
