using Site.Business.Interfaces;
using Site.Business.Model;
using Site.Data.Context;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Site.Data.Repository
{
    public class DistribuidoraRepository : Repository<Distribuidora>, IDistribuidoraRepository
    {
        public DistribuidoraRepository(MeuDbContext context) : base(context)
        {

        }

        public async Task<Distribuidora> ObterDistribuidoraPorId(Guid id)
        {
            return await Db.Distribuidoras.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);

        }

        public async Task<Distribuidora> ObterDistribuidoraProduto(Guid id)
        {
            return await Db.Distribuidoras.AsNoTracking().Include(f => f.Filmes).FirstOrDefaultAsync(d=> d.Id == id);
        }
    }
}
