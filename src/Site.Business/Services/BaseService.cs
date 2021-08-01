using Site.Business.Interfaces;
using System.Collections.Generic;

using FluentValidation;
using FluentValidation.Results;
using System.Text;
using Site.Business.Model;
using Site.Business.Notificacoes;

namespace Site.Business.Services
{
    public abstract class BaseService
    {

        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validation = validacao.Validate(entidade);

            if(validation.IsValid)
            {
                return true;
            }

            Notificar(validation);
            return false;

        }

    }
}
