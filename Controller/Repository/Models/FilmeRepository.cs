using Domain.Models;
using Repository.Context;

namespace Repository.Models
{
    public class FilmeRepository : Repository<Filme>
    {
        public FilmeRepository(BaseContext ctx) : base(ctx) { }

    }
}
