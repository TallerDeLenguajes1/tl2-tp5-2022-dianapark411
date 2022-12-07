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
            
            CreateMap<Pedido, PedidoViewModel>();
            CreateMap<PedidoViewModel, Pedido>();
        }
    }
}
