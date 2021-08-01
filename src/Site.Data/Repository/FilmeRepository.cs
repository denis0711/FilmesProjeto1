using Site.Business.Interfaces;
using Site.Business.Model;
using Site.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Site.Data.Repository
{
    public class FilmeRepository : Repository<Filme> , IFilmesRepository
    {
        public FilmeRepository(MeuDbContext context) : base (context)
        {

        }

        public async Task<IEnumerable<Filme>>ObterFilmesPorDistribuidora(Guid id)
        {
           return await Buscar(f => f.DistribuidoraId == id);
        }

        public async Task<IEnumerable<Filme>> ObterFilmesDistribuidora()
        {
            return await Db.Filmes.AsNoTracking().Include(d => d.Distribuidora).OrderBy(f => f.Nome).ToListAsync();
        }

        public async Task<Filme> ObterFilmeDistribuidora(Guid id)
        {
            return await Db.Filmes.AsNoTracking().Include(d => d.Distribuidora).FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
