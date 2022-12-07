using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using AutoMapper;
using WebCadeteria.ViewModels;
using System.Text;

namespace WebCadeteria.Controllers{
    public class ClienteController : Controller
    {

        private string path_clientes = @"Recursos\clientes.csv";
        private Cadeteria _cadeteria = new Cadeteria();
        private Helper _helper = new Helper();

        private readonly ILogger<HomeController> _logger;

        private readonly IMapper _mapper;

        public ClienteController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _helper.leerClientes(path_clientes, _cadeteria.ListadoClientes);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CargarCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CargarCliente(ClienteViewModel _clienteVM)
        {
            int id_actual = _cadeteria.ListadoClientes.Count();
            if(ModelState.IsValid){         
                Cliente cli = _mapper.Map<Cliente>(_clienteVM);
                System.IO.File.AppendAllText(path_clientes, $"{id_actual};{cli.Nombre};{cli.Direccion};{cli.Telefono};{cli.DatosReferenciaDireccion}\n");
                _cadeteria.ListadoClientes.Add(cli);
                return View("ListarClientes");
            }else{
                return View();
            }
        }

        public IActionResult MostrarClientes()
        {
            return View("ListarClientes");
        }
        

        public IActionResult BajarCadete(int id){
            string[] leer = System.IO.File.ReadAllLines(path_clientes);
            System.IO.File.WriteAllText(path_clientes,"");
            
            for (int i = 0; i < leer.Length; i++){
                string[] datos = leer[i].Split(";");
                if (id == Convert.ToInt32(datos[0])){   
                    _cadeteria.ListadoClientes.RemoveAt(id);

                    for(int j = id-1; j < leer.Length -2; j++){ //length -2 porque ya hice un remove y se resta 1, y porque adentro hago j+1 para que nose se pase del rango
                        System.IO.File.AppendAllText(path_clientes, $"{j+1};{ _cadeteria.ListadoClientes[j+1].Nombre};{ _cadeteria.ListadoClientes[j+1].Direccion};{ _cadeteria.ListadoClientes[j+1].Telefono};{ _cadeteria.ListadoClientes[j+1].DatosReferenciaDireccion}" + "\n");
                    }
                    break;
                }else{
                    System.IO.File.AppendAllText(path_clientes, leer[i] +"\n");  
                } 
            }

            return View("ListarClientes");
        }

        [HttpGet]
        public IActionResult ModificarCliente(int id)
        {   
            Cliente cli = _cadeteria.ListadoClientes[id];
            ClienteViewModel _clienteVM = _mapper.Map<ClienteViewModel>(cli);
            return View(_clienteVM);
        }

        [HttpPost]
        public IActionResult ModificarCliente(ClienteViewModel _clienteVM)
        {
            Cliente cli = _mapper.Map<Cliente>(_clienteVM);
            _cadeteria.ListadoClientes[cli.Id] = cli;

            string[] leer = System.IO.File.ReadAllLines(path_clientes);
            System.IO.File.WriteAllText(path_clientes,"");
            

            for (int i = 0; i < leer.Length; i++){
                string[] datos = leer[i].Split(";");
                if (cli.Id == Convert.ToInt32(datos[0])){   
                    System.IO.File.AppendAllText(path_clientes, $"{cli.Id};{cli.Nombre};{cli.Direccion};{cli.Telefono};{cli.DatosReferenciaDireccion}" + "\n");
                }else{
                    System.IO.File.AppendAllText(path_clientes, leer[i] +"\n");  
                } 
            }
            return View("ListarClientes");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
