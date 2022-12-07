using AutoMapper;
using WebCadeteria.ViewModels;
using WebCadeteria.Models;

namespace WebCadeteria.Mapper{
    public class MappingProfile: Profile{
        public MappingProfile(){
            CreateMap<CadeteViewModel, Cadete>();
            CreateMap<Cadete, CadeteViewModel>();

            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<Cliente, ClienteViewModel>();
            
            //Como el cliente en Pedido es un objeto y en el viewmodel es un string indico que mapee el telefono:
            CreateMap<Pedido, PedidoViewModel>()
            .ForMember( dest => dest.Cliente, input => input.MapFrom( i => i.Cliente.Telefono))
            .ReverseMap();
        }
    }
}
