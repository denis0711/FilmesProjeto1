using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Site.App.Extensions;
using Site.Business.Interfaces;
using Site.Business.Notificacoes;
using Site.Business.Services;
using Site.Data.Context;
using Site.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.App.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {

            services.AddScoped<MeuDbContext>();
            services.AddScoped<IDistribuidoraRepository, DistribuidoraRepository>();
            services.AddScoped<IFilmesRepository, FilmeRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IDistribuidoraService, DistribuidoraService>();
            services.AddScoped<IFilmeService, FilmeService>();

            return services;
        }
    }
}
