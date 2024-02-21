using AutoMapper;
using WebCadeteria.ViewModels;
using WebCadeteria.Models;

namespace WebCadeteria.Mapper{
    public class MappingProfile: Profile{
        public MappingProfile(){

            //Mapeos de Cadete
            CreateMap<CadeteViewModel, Cadete>();
            CreateMap<Cadete, CadeteViewModel>();

            CreateMap<CargarCadeteViewModel, Cadete>();
            CreateMap<Cadete, CargarCadeteViewModel>();

            CreateMap<ModificarCadeteViewModel, Cadete>();
            CreateMap<Cadete, ModificarCadeteViewModel>();

            //Mapeos de Cliente
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<Cliente, ClienteViewModel>(); 

            CreateMap<CargarClienteViewModel, Cliente>();
            CreateMap<Cliente, CargarClienteViewModel>();   

            CreateMap<ModificarClienteViewModel, Cliente>();
            CreateMap<Cliente, ModificarClienteViewModel>();            
            
            //Mapeos de Pedido
            CreateMap<PedidoViewModel, Pedido>();
            CreateMap<Pedido, PedidoViewModel>();

            CreateMap<AgregarPedidoViewModel, Pedido>();
            CreateMap<Pedido, AgregarPedidoViewModel>();

            CreateMap<ModificarPedidoViewModel, Pedido>();
            CreateMap<Pedido, ModificarPedidoViewModel>();

            //Mapeos de Usuario
            CreateMap<CargarUsuarioViewModel, Usuario>();
            CreateMap<Usuario, CargarUsuarioViewModel>();

            CreateMap<ModificarUsuarioViewModel, Usuario>();
            CreateMap<Usuario, ModificarUsuarioViewModel>();

        }
    }
}
