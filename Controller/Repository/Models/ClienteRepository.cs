using Domain.Models;
using Repository.Context;

namespace Repository.Models
{
    public class ClienteRepository : Repository<Cliente>
    {
        public ClienteRepository(BaseContext ctx) : base(ctx) { }
    }
}
