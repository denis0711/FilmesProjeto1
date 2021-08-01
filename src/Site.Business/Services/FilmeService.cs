using Site.Business.Interfaces;
using System;
using System.Threading.Tasks;
using Site.Business.Model;
using Site.Business.Validations;

namespace Site.Business.Services
{
    public class FilmeService : BaseService, IFilmeService
    {
        private readonly IFilmesRepository filmesRepository;

        public FilmeService( IFilmesRepository filmesRepository, INotificador notificador) : base(notificador)
        {
        
            this.filmesRepository = filmesRepository;
        }

        public async Task Adicionar(Filme filme)
        {
            if (!ExecutarValidacao(new FilmeValidation(), filme)) return;

            await filmesRepository.Adicionar(filme);
        }

        public async Task Atualizar(Filme filme)
        {
            if (!ExecutarValidacao(new FilmeValidation(), filme)) return;

            await filmesRepository.Atualizar(filme);
        }

        public async Task Remover(Guid id)
        {
            await filmesRepository.Remover(id);
        }

        public void Dispose()
        {
            filmesRepository?.Dispose();
        }
    }
}
