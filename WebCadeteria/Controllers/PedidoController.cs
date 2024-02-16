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
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            if(es_admin()){
                return RedirectToAction("MostrarPedidos");
            }else{
                int id_cadete = (int)HttpContext.Session.GetInt32("Id_cadete");
                
                return RedirectToAction("MostrarPedidosCadete", new { cadete = id_cadete});
            }  
        }

        [HttpGet]
        public IActionResult AgregarPedido()
        {
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            if(es_admin()){
                return View();
            }else{
                return View("../Sesion/ErrorPermisos");
            }
        }

        [HttpPost]
        public IActionResult AgregarPedido(PedidoViewModel _pedidoVM){

            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            if(ModelState.IsValid){         
                if(es_admin()){
                    Pedido ped = _mapper.Map<Pedido>(_pedidoVM);
                    _repository.Insert(ped);

                    return RedirectToAction("Index");
                }else{
                    return View("../Sesion/ErrorPermisos");
                }
            }else{
                return View();
            }
        }


        public IActionResult MostrarPedidos(){
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }

            if(es_admin()){
                return View("ListarPedidos", _repository.FindAll());
            }else{
                return View("../Sesion/ErrorPermisos");
            }
        }

        public IActionResult MostrarPedidosCadete(int cadete){
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            return View("ListarPedidos", _repository.FindByIdCadete(cadete));
        }

        public IActionResult BajarPedido(int id){
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }

            if(es_admin()){
                _repository.Delete(id);
                return RedirectToAction("Index");
            }else{
                return View("../Sesion/ErrorPermisos");
            }    
        }

        [HttpGet]
        public IActionResult ModificarPedido(int id)
        {   
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            
            //Si es Cadete o Admin puede modificar el pedido
            Pedido ped = _repository.FindById(id);
            PedidoViewModel _pedidoVM = _mapper.Map<PedidoViewModel>(ped);
            return View(_pedidoVM);
        }

        [HttpPost]
        public IActionResult ModificarPedido(PedidoViewModel _pedidoVM) 
        {   
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            if(ModelState.IsValid){
                Pedido ped = _mapper.Map<Pedido>(_pedidoVM);
                _repository.Update(ped);

                return RedirectToAction("Index");  
            }else{
                return View();
            }
        }

        private bool esta_logueado(){
            return HttpContext.Session.Keys.Any();
        }

        private bool es_admin(){
            return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("Rol") == "Administrador";
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
