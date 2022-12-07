using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using AutoMapper;

namespace WebCadeteria.Controllers{
    public class PedidoController : Controller
    {   
        private string path_pedidos = @"Recursos\pedidos.csv";

        private Cadeteria _cadeteria = new Cadeteria();

        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        public PedidoController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        
        public IActionResult Index()
        {         
            return View();
        }

        public IActionResult AgregarPedido()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarPedido(PedidoViewModel _pedidoVM){

            int id_actual = _cadeteria.ListadoPedidos.Count();
            
            if(ModelState.IsValid){                
                Pedido ped= _mapper.Map<Pedido>(_pedidoVM);
                System.IO.File.AppendAllText(path_pedidos, $"{id_actual};{ped.Obs};{ped.Estado};{_pedidoVM.Cliente}\n");

                Cliente cli;
                bool encontrado;
                //el cliente en _pedidoVM es un string
                (cli,encontrado) = _cadeteria.buscarCliente(_pedidoVM.Cliente);
                if(encontrado){
                    ped.Cliente = cli;
                }
                _cadeteria.ListadoPedidos.Add(ped);
                return View("ListarPedidos");
            }else{
                return View();
            }
        }


        public IActionResult MostrarPedidos(){
            return View("ListarPedidos");
        }

        public IActionResult BajarPedido(int id){
            string[] leer = System.IO.File.ReadAllLines(path_pedidos);
            System.IO.File.WriteAllText(path_pedidos,"");
            
            for (int i = 0; i < leer.Length; i++){
                string[] datos = leer[i].Split(";");
                if (id == Convert.ToInt32(datos[0])){ 
                    _cadeteria.ListadoPedidos.RemoveAt(id);

                    for(int j = id-1; j < leer.Length -2; j++){ //length -2 porque ya hice un remove y se resta 1, y porque adentro hago j+1 para que nose se pase del rango
                        System.IO.File.AppendAllText(path_pedidos, $"{j+1};{ _cadeteria.ListadoPedidos[j+1].Obs};{ _cadeteria.ListadoPedidos[j+1].Estado};{ _cadeteria.ListadoPedidos[j+1].Cliente}" + "\n");
                    }
                    break;
                }else{
                    System.IO.File.AppendAllText(path_pedidos, leer[i] +"\n");  
                } 
            }

            return View("ListarPedidos");
        }

        [HttpGet]
        public IActionResult ModificarPedido(int id)
        {   
            Pedido ped = _cadeteria.ListadoPedidos[id];
            PedidoViewModel _pedidoVM = _mapper.Map<PedidoViewModel>(ped);
            return View(_pedidoVM);
        }

        [HttpPost]
        public IActionResult ModificarPedido(PedidoViewModel _pedidoVM)
        {
            //porque el nro se pone en 0???? :(
            Pedido ped = _mapper.Map<Pedido>(_pedidoVM);
            Cliente cli;
            bool encontrado;
            (cli,encontrado) = _cadeteria.buscarCliente(_pedidoVM.Cliente);
            if(encontrado){
                ped.Cliente = cli;
            }

            _cadeteria.ListadoPedidos[ped.Nro] = ped;

            string[] leer = System.IO.File.ReadAllLines(path_pedidos);
            System.IO.File.WriteAllText(path_pedidos,"");
            

            for (int i = 0; i < leer.Length; i++){
                string[] datos = leer[i].Split(";");
                if (ped.Nro == Convert.ToInt32(datos[0])){   
                    System.IO.File.AppendAllText(path_pedidos, $"{ped.Nro};{ped.Obs};{ped.Estado};{_pedidoVM.Cliente}" + "\n");
                }else{
                    System.IO.File.AppendAllText(path_pedidos, leer[i] +"\n");  
                } 
            }
            return View("ListarPedidos");
        }


        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
