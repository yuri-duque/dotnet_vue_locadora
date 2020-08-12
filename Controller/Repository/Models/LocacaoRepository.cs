using Domain.Models;
using Repository.Context;
using System;
using System.Linq;

namespace Repository.Models
{
    public class LocacaoRepository : Repository<Locacao>
    {
        public LocacaoRepository(BaseContext ctx) : base(ctx) { }

        public Locacao EncontrarLocacao(int id)
        {
            var result = Find(id);

            if (result is null)
                throw new Exception("Locacao não encontrada! Verifique se IdCliente e o IdFilme estão corretos.");
            else
                Detached(result);

            return result;
        }

        public IQueryable<Locacao> GetLocacoesAtivasByCliente(int idCliente)
        {
            return GetAll()
                .Where(x => x.DataDevolucao == null && x.IdCliente == idCliente);
        }

        public IQueryable<Locacao> GetLocacoesAtivasByFilme(int idFilme)
        {
            return GetAll()
                .Where(x => x.DataDevolucao == null && x.IdFilme == idFilme);
        }
    }
}
