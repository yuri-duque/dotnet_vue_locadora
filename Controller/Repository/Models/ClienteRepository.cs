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

        public void EncontrarCliente(int id, string CPF)
        {
            var result = Find(id, CPF);

            if (result is null)
                throw new Exception("Cliente não encontrado! Verifique se Id e o CPF estão corretos.");
            else
                Detached(result);
        }

        public Cliente GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public Cliente GetByCPF(string CPF)
        {
            return GetAll().FirstOrDefault(x => x.CPF.Equals(CPF));
        }

        public Cliente GetLocacoesAtivasByCliente(int id)
        {
            return GetAll()
                .Include(x => x.Locacoes)
                .SelectMany(x => x.Locacoes)
                .Where(x => !x.FilmeDevolvido)
                .Select(x => x.Cliente)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
