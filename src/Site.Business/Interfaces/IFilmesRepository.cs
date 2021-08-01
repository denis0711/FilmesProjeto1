using Site.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Business.Interfaces
{
    public interface IFilmesRepository : IRepository<Filme>
    {

        Task<IEnumerable<Filme>> ObterFilmesPorDistribuidora(Guid id);

        Task<IEnumerable<Filme>> ObterFilmesDistribuidora();
        Task<Filme> ObterFilmeDistribuidora(Guid id);
    }


}
