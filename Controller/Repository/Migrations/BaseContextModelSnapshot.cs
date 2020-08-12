﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Context;

namespace Repository.Migrations
{
    [DbContext(typeof(BaseContext))]
    partial class BaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.HasKey("Id", "CPF");

                    b.ToTable("clientes");
                });

            modelBuilder.Entity("Domain.Models.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<int>("ClassificacaoIndicativa")
                        .HasColumnType("int");

                    b.Property<short>("Lancamento")
                        .HasColumnType("smallint");

                    b.HasKey("Id", "Titulo");

                    b.ToTable("filmes");
                });

            modelBuilder.Entity("Domain.Models.Locacao", b =>
                {
                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdFilme")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataDevolucao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataLocacao")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdCliente", "IdFilme");

                    b.HasIndex("IdFilme");

                    b.ToTable("locacoes");
                });

            modelBuilder.Entity("Domain.Models.Locacao", b =>
                {
                    b.HasOne("Domain.Models.Cliente", "Cliente")
                        .WithMany("Locacoes")
                        .HasForeignKey("IdCliente")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Filme", "Filme")
                        .WithMany("Locacoes")
                        .HasForeignKey("IdFilme")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
