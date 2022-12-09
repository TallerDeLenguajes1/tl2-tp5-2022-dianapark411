using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using AutoMapper;

namespace WebCadeteria.Controllers{
    public class PedidoController : Controller
    {   
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        private readonly IPedido _repository;
        public PedidoController(ILogger<HomeController> logger, IMapper mapper, IPedido repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        
        public IActionResult Index()
        {         
            return View("ListarPedidos", _repository.FindAll());
        }

        [HttpGet]
        public IActionResult AgregarPedido()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarPedido(PedidoViewModel _pedidoVM){

            if(ModelState.IsValid){         
                Pedido ped = _mapper.Map<Pedido>(_pedidoVM);
                _repository.Insert(ped);

                return RedirectToAction("Index");
            }else{
                return View();
            }
        }


        public IActionResult MostrarPedidos(){
            return RedirectToAction("Index");
        }

        public IActionResult BajarPedido(int id){
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ModificarPedido(int id)
        {   
            Pedido ped = _repository.FindById(id);
            PedidoViewModel _pedidoVM = _mapper.Map<PedidoViewModel>(ped);
            return View(_pedidoVM);
        }

        [HttpPost]
        public IActionResult ModificarPedido(PedidoViewModel _pedidoVM) //revisar porque se cambia el id cuando lo modifico
        {
            if(ModelState.IsValid){
                
                Pedido ped = _mapper.Map<Pedido>(_pedidoVM);
                _repository.Update(ped);

                return RedirectToAction("Index");
            }else{
                return View();
            }
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
