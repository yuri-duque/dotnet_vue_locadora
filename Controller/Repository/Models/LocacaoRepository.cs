using Domain.Models;
using Repository.Context;
using System;

namespace Repository.Models
{
    public class LocacaoRepository : Repository<Locacao>
    {
        public LocacaoRepository(BaseContext ctx) : base(ctx) { }

        public Locacao EncontrarLocacao(int idCliente, int idFilme)
        {
            var result = Find(idCliente, idFilme);

            if (result is null)
                throw new Exception("Locacao não encontrada! Verifique se IdCliente e o IdFilme estão corretos.");
            else
                Detached(result);

            return result;
        }
    }
}
