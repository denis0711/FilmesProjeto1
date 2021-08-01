using AutoMapper;
using Site.App.Models;
using Site.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Filme, FilmeViewModel>().ReverseMap();
            CreateMap<Distribuidora, DistribuidoraViewModel>().ReverseMap();
        }
    }
}
