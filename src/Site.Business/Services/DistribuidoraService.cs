using Site.Business.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Site.Business.Model;
using Site.Business.Validations;

namespace Site.Business.Services
{
    public class DistribuidoraService : BaseService, IDistribuidoraService
    {
        private readonly IDistribuidoraRepository distribuidoraRepository;

        public DistribuidoraService( IDistribuidoraRepository distribuidoraRepository,
                                 INotificador notificador) : base(notificador)
        {
         
            this.distribuidoraRepository = distribuidoraRepository;
        }

        public async Task Adicionar(Distribuidora distribuidora)
        {
            if (!ExecutarValidacao(new DistribuidoraValidation(), distribuidora))return;

            await distribuidoraRepository.Adicionar(distribuidora);
        }

        public async Task Atualizar(Distribuidora distribuidora)
        {
            if (!ExecutarValidacao(new DistribuidoraValidation(), distribuidora)) return;


            await distribuidoraRepository.Atualizar(distribuidora);
        }

        public void Dispose()
        {
            distribuidoraRepository?.Dispose();
        }

        public async Task Remover(Guid id)
        {
            if (distribuidoraRepository.ObterDistribuidoraPorId(id).Result.Filmes.Any())
            {
                Notificar("A distribuidora possui Filmes cadastrados!");
                return;
            }

            await distribuidoraRepository.Remover(id);

            
        }
    }
}
