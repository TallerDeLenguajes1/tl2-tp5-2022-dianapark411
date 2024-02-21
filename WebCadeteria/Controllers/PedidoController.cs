using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using AutoMapper;

//Agrego para poder usar SelectList para el ViegBag
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebCadeteria.Controllers{
    public class PedidoController : Controller
    {   
        private readonly ILogger<PedidoController> _logger;
        
        private readonly IMapper _mapper;
        private readonly IPedido _repository;
        private readonly ICliente _repository_cliente;
        private readonly ICadete _repository_cadete;

        public PedidoController(ILogger<PedidoController> logger, IMapper mapper, IPedido repository, ICliente repository_cliente,ICadete repository_cadete)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _repository_cliente = repository_cliente;
            _repository_cadete = repository_cadete;
        }
        
        public IActionResult Index()
        {   
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                if(es_admin()){
                    return RedirectToAction("MostrarPedidos");
                }else{
                    int id_cadete = (int)HttpContext.Session.GetInt32("Id_cadete");
                    
                    return RedirectToAction("MostrarPedidosCadete", new { cadete = id_cadete});
                }
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }    
        }

        [HttpGet]
        public IActionResult AgregarPedido()
        {
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                if(es_admin()){
                    
                    //Uso ViewBags para pasarle a la View tanto el Id como el nombre de los clientes y cadetes
                    //En la vista se usarán para desplegar los clientes y cadetes disponibles para elegir a la hora de cargar un pedido
                    var clientes = _repository_cliente.FindAll();
                    ViewBag.ClientesList = new SelectList(clientes, "Id", "Nombre");

                    var cadetes = _repository_cadete.FindAll();
                    ViewBag.CadetesList = new  SelectList(cadetes, "Id", "Nombre");

                    return View();
                }else{
                    return View("../Sesion/ErrorPermisos");
                }
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult AgregarPedido(PedidoViewModel _pedidoVM){
            try{
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
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult MostrarPedidos(){
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }

                if(es_admin()){
                    return View("ListarPedidos", _repository.FindAll());
                }else{
                    return View("../Sesion/ErrorPermisos");
                }
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            } 
        }

        [HttpGet]
        public IActionResult MostrarPedidosCadete(int cadete){
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                return View("ListarPedidos", _repository.FindByIdCadete(cadete));
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult BajarPedido(int id){
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }

                if(es_admin()){
                    _repository.Delete(id);
                    return RedirectToAction("Index");
                }else{
                    return View("../Sesion/ErrorPermisos");
                }   
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }     
        }

        [HttpGet]
        public IActionResult ModificarPedido(int id)
        {   
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }

                //Si es Cadete o Admin puede modificar el pedido
                var clientes = _repository_cliente.FindAll();
                ViewBag.ClientesList = new SelectList(clientes, "Id", "Nombre");

                var cadetes = _repository_cadete.FindAll();
                ViewBag.CadetesList = new  SelectList(cadetes, "Id", "Nombre");
    
                Pedido ped = _repository.FindById(id);
                PedidoViewModel _pedidoVM = _mapper.Map<PedidoViewModel>(ped);
                return View(_pedidoVM);
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            } 
        }

        [HttpPost]
        public IActionResult ModificarPedido(PedidoViewModel _pedidoVM) 
        {   
            try{
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
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
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
