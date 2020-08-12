using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System;
using System.Linq;

namespace Repository.Models
{
    public class ClienteRepository : Repository<Cliente>
    {
        public ClienteRepository(BaseContext ctx) : base(ctx) { }

        public Cliente EncontrarCliente(int id)
        {
            var result = Find(id);

            if (result is null)
                throw new Exception("Cliente não encontrado! Verifique se Id e o CPF estão corretos.");
            else
                Detached(result);

            return result;
        }

        public Cliente GetByCPF(string CPF)
        {
            return GetAll().FirstOrDefault(x => x.CPF.Equals(CPF));
        }

        public IQueryable<Cliente> Relatorio(bool isAtrasados, int? indexRecordistas)
        {
            var list = GetAll();

            if (isAtrasados)
            {
                list = list
                    .SelectMany(x => x.Locacoes)
                    .Include(x => x.Filme)
                    .Where(x =>
                        (x.DataDevolucao > (x.Filme.Lancamento ? x.DataLocacao.AddDays(2) : x.DataLocacao.AddDays(3)))
                        || (x.DataDevolucao == null && (x.Filme.Lancamento ? x.DataLocacao.AddDays(2) : x.DataLocacao.AddDays(3)) < DateTime.Now))
                    .Select(x => x.Cliente);
            }

            if(indexRecordistas != null && indexRecordistas > 0)
            {
                list = list
                    .Include(x => x.Locacoes)
                    .OrderBy(x => x.Locacoes.Count());
            }

            return list;
        }

        private DateTime DataDevolucaoCorreta(DateTime dataLocacao, bool lancamento)
        {
            if (lancamento)
                return dataLocacao.AddDays(2);
            else
                return dataLocacao.AddDays(3);
        }
    }
}
