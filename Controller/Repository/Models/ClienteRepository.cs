using Domain.Models;
using Repository.Context;
using System;

namespace Repository.Models
{
    public class ClienteRepository : Repository<Cliente>
    {
        public ClienteRepository(BaseContext ctx) : base(ctx) { }

        public void EncontrarCliente(int id, string CPF)
        {
            var result = Find(id, CPF);

            if (result is null)
                throw new Exception("Cliente não encontrado! Verifique se Id e o CPF estão corretos.");
            else
                Detached(result);
        }
    }
}
