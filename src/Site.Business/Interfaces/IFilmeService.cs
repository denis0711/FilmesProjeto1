using Site.Business.Model;
using System;
using System.Threading.Tasks;

namespace Site.Business.Interfaces
{
    public interface IFilmeService
    {
        Task Atualizar(Filme filme);
        Task Adicionar(Filme filme);

        Task Remover(Guid id);

    }
}
