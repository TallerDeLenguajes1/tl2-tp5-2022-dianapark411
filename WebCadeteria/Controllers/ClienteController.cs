using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using AutoMapper;
using WebCadeteria.ViewModels;
using System.Text;

namespace WebCadeteria.Controllers{
    public class ClienteController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly ICliente _repository;

        public ClienteController(ILogger<HomeController> logger, IMapper mapper, ICliente repository)
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
                return View("ListarClientes", _repository.FindAll());
            }else{
                return View("../Sesion/ErrorPermisos");
            }
        }

        [HttpGet]
        public IActionResult CargarCliente()
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
        public IActionResult CargarCliente(ClienteViewModel _clienteVM)
        {
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
        }

        public IActionResult MostrarClientes()
        {
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }

            if(es_admin()){
                return RedirectToAction("Index"); //para que index pase el viewmodel
            }else{
                return View("../Sesion/ErrorPermisos");
            }
        }
        

        public IActionResult BajarCliente(int id){
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
        public IActionResult ModificarCliente(int id)
        {   
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            
            if(es_admin()){
                Cliente cli = _repository.FindById(id);
                ClienteViewModel _clienteVM = _mapper.Map<ClienteViewModel>(cli);
                return View(_clienteVM);
            }else{
                return View("../Sesion/ErrorPermisos");
            }
            
        }

        [HttpPost]
        public IActionResult ModificarCliente(ClienteViewModel _clienteVM)
        {
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
        }

        [HttpGet]
        public IActionResult BuscarCliente(int id)
        {   
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            Cliente cli = _repository.FindById(id);
            ClienteViewModel _clienteVM = _mapper.Map<ClienteViewModel>(cli);
            return View(_clienteVM);
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
