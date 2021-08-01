using Site.Business.Model;
using System;
using System.Threading.Tasks;

namespace Site.Business.Interfaces
{
    public interface IDistribuidoraRepository : IRepository<Distribuidora>
    {
        Task<Distribuidora> ObterDistribuidoraPorId(Guid id);
        Task<Distribuidora> ObterDistribuidoraProduto(Guid id);

    }


}
