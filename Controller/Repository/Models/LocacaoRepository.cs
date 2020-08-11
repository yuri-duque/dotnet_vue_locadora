using Domain.Models;
using Repository.Context;

namespace Repository.Models
{
    public class LocacaoRepository : Repository<Locacao>
    {
        public LocacaoRepository(BaseContext ctx) : base(ctx) { }
    }
}
