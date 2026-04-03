using AutoMapper;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;

namespace MoneyWeb.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UsuarioViewModel, Usuario>()
                .ForMember(dest => dest.Categorias, opt => opt.Ignore())
                .ForMember(dest => dest.Contas, opt => opt.Ignore())
                .ForMember(dest => dest.Lancamentos, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Senha, opt =>
                {   //ignora se a senha chegar null
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.Senha));
                    opt.MapFrom(src => PasswordHelper.HashPassword(src.Senha));
                });

            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(dest => dest.Senha, opt => opt.Ignore());

            CreateMap<Categoria, CategoriaViewModel>();

            CreateMap<CategoriaViewModel, Categoria>()
                .ForMember(dest => dest.Lancamentos, opt => opt.Ignore())
                .ForMember(dest => dest.Usuario, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
                .ForMember(dest => dest.Limite, opt => opt.Ignore());

            CreateMap<Conta, ContaViewModel>();

            CreateMap<ContaViewModel, Conta>()
                .ForMember(dest => dest.Cartoes, opt => opt.Ignore())
                .ForMember(dest => dest.Lancamentos, opt => opt.Ignore())
                .ForMember(dest => dest.Usuario, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore());

            CreateMap<Cartao, CartaoViewModel>()
                .ForMember(dest => dest.ContaNome, opt => opt.MapFrom(src => src.Conta.Nome));

            CreateMap<CartaoViewModel, Cartao>()
                .ForMember(dest => dest.Lancamentos, opt => opt.Ignore())
                .ForMember(dest => dest.Conta, opt => opt.Ignore());

            CreateMap<Limite, LimiteViewModel>()
                .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria.Nome));

            CreateMap<LimiteViewModel, Limite>()
                .ForMember(dest => dest.Categoria, opt => opt.Ignore());
        }
    }
}