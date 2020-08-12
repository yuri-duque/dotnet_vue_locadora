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
                    .Where(x => !x.FilmeDevolvido && x.DataDevolucao < DateTime.Now)
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
    }
}
