using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Text;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;


namespace WebCadeteria.Controllers{
    public class ClienteController : Controller
    {

        private readonly ILogger<ClienteController> _logger;
        private readonly IMapper _mapper;
        private readonly ICliente _repository;

        public ClienteController(ILogger<ClienteController> logger, IMapper mapper, ICliente repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        public IActionResult Index()
        {
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                if(es_admin()){
                    return View("ListarClientes", _repository.FindAll());
                }else{
                    return View("../Sesion/ErrorPermisos");
                    //TempData ["Message"] = "No cuenta con los permisos necesarios para realizar esta acción, comuníquese con el administrador";
                    //return RedirectToAction("Index", "Home");
                }
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            } 
        }

        [HttpGet]
        public IActionResult CargarCliente()
        {
            try{
                 if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                if(es_admin()){
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
        public IActionResult CargarCliente(CargarClienteViewModel _clienteVM)
        {
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                if(ModelState.IsValid){        
                    if(es_admin()){
                        Cliente cli = _mapper.Map<Cliente>(_clienteVM);
                        _repository.Insert(cli);

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
        
        [HttpPost]
        public IActionResult BajarCliente(int id){
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
        public IActionResult ModificarCliente(int id)
        {   
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                
                if(es_admin()){
                    Cliente cli = _repository.FindById(id);
                    ModificarClienteViewModel _clienteVM = _mapper.Map<ModificarClienteViewModel>(cli);
                    return View(_clienteVM);
                }else{
                    return View("../Sesion/ErrorPermisos");
                }
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult ModificarCliente(ModificarClienteViewModel _clienteVM)
        {
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                if(ModelState.IsValid){
                    if(es_admin()){
                        
                        Cliente cli = _mapper.Map<Cliente>(_clienteVM);
                        _repository.Update(cli);

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
        public IActionResult MostrarClientes()
        {
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }

                if(es_admin()){
                    return RedirectToAction("Index"); //para que index pase el viewmodel
                }else{
                    return View("../Sesion/ErrorPermisos");
                }
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }
        
        [HttpGet]
        public IActionResult BuscarCliente(int id)
        {   
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                Cliente cli = _repository.FindById(id);
                ClienteViewModel _clienteVM = _mapper.Map<ClienteViewModel>(cli);
                return View(_clienteVM);
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
