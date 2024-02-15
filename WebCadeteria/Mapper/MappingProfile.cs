using AutoMapper;
using WebCadeteria.ViewModels;
using WebCadeteria.Models;

namespace WebCadeteria.Mapper{
    public class MappingProfile: Profile{
        public MappingProfile(){

            //Mapeos de Cadete
            CreateMap<CadeteViewModel, Cadete>();
            CreateMap<Cadete, CadeteViewModel>();

            //Mapeos de Cliente
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<Cliente, ClienteViewModel>();            
            
            //Mapeos de Pedido
            CreateMap<PedidoViewModel, Pedido>();
            CreateMap<Pedido, PedidoViewModel>();

            //Mapeos de Usuario
            CreateMap<CargarUsuarioViewModel, Usuario>();
            CreateMap<Usuario, CargarUsuarioViewModel>();

            CreateMap<ModificarUsuarioViewModel, Usuario>();
            CreateMap<Usuario, ModificarUsuarioViewModel>();



        }
    }
}
