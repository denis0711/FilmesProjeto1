using Site.Business.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Business.Interfaces
{
    public interface IDistribuidoraService : IDisposable
    {
        Task Atualizar(Distribuidora distribuidora);
        Task Adicionar(Distribuidora distribuidora);

        Task Remover(Guid id);
    }
}
