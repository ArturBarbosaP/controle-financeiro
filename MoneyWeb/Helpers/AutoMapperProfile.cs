using AutoMapper;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;

namespace MoneyWeb.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UsuarioViewModel, Usuario>();
        }
    }
}