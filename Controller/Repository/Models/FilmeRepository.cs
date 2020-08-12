using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System;
using System.Linq;

namespace Repository.Models
{
    public class FilmeRepository : Repository<Filme>
    {
        public FilmeRepository(BaseContext ctx) : base(ctx) { }

        public Filme EncontrarFilme(int id)
        {
            var result = Find(id);

            if (result is null)
                throw new Exception("Filme não encontrado! Verifique se Id e o Titulo estão corretos.");
            else
                Detached(result);

            return result;
        }

        public Filme GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public Filme GetByTitulo(string titulo)
        {
            return GetAll().FirstOrDefault(x => x.Titulo.Equals(titulo));
        }

        public IQueryable<Filme> Relatorio(bool isNuncaAlugados, bool? maisAlugados = null, DateTime? periodoMaisAlugados = null, int? quantidadeFilmes = null)
        {
            var list = GetAll();

            // selecionar filmes que nunca foram alugados
            if (isNuncaAlugados)
            {
                list = list
                    .Where(x => x.Locacoes.Count() == 0);
            }

            // selecionar o filmes que foram alugados em determinado periodo
            if(periodoMaisAlugados != null)
            {
                list = list
                    .SelectMany(x => x.Locacoes)
                    .Where(x => x.DataLocacao > Convert.ToDateTime(periodoMaisAlugados))
                    .Select(x => x.Filme)
                    .Distinct();
            }

            // selecionar os mais ou menos alugados
            if(maisAlugados == true)
            {
                list = list.OrderBy(x => x.Locacoes.Count());
            }
            else if(maisAlugados == false)
            {
                list = list.OrderByDescending(x => x.Locacoes.Count());
            }

            // pegar determinado numero de filmes na listagem
            if (quantidadeFilmes != null)
            {
                list = list.Take(Convert.ToInt32(quantidadeFilmes));
            }

            return list;
        }
    }
}
