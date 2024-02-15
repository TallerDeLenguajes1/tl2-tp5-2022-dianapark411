using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;

//Administrador :ABM y listado de cadetes, Pedidos y Clientes -->
//Cadete: Listado de pedidos del cadete, y cambio de estado de pedidos del cadete. --> 

namespace WebCadeteria.Controllers{
    public class CadeteController : Controller
    {       
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly ICadete _repository;

        public CadeteController(ILogger<HomeController> logger, IMapper mapper, ICadete repository)
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
                return View("ListarCadetes", _repository.FindAll());
            }else{
                return View("../Sesion/ErrorPermisos");
            }
        }

        [HttpGet]
        public IActionResult CargarCadete()
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
        public IActionResult CargarCadete(CadeteViewModel _cadeteVM)
        {
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            if(ModelState.IsValid){
                if(es_admin()){
                    Cadete cad = _mapper.Map<Cadete>(_cadeteVM);
                    _repository.Insert(cad);

                    return RedirectToAction("Index");
                }else{
                    return View("../Sesion/ErrorPermisos");
                }
            }else{
                return View();
            }  
        }

        public IActionResult MostrarCadetes(){
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }

            if(es_admin()){
                return RedirectToAction("Index"); //para que index pase el viewmodel
            }else{
                return View("../Sesion/ErrorPermisos");
            }
            
        }

        public IActionResult BajarCadete(int id){
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
        public IActionResult ModificarCadete(int id)
        {   
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            
            if(es_admin()){
                Cadete cad = _repository.FindById(id);
                CadeteViewModel _cadeteVM = _mapper.Map<CadeteViewModel>(cad);
                return View(_cadeteVM);
            }else{
                return View("../Sesion/ErrorPermisos");
            }   
        }

        [HttpPost]
        public IActionResult ModificarCadete(CadeteViewModel _cadeteVM)
        {
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }

            if(ModelState.IsValid){
                if(es_admin()){
                    
                    Cadete cad = _mapper.Map<Cadete>(_cadeteVM);
                    _repository.Update(cad);

                    return RedirectToAction("Index");
                }else{
                    return View("../Sesion/ErrorPermisos");
                }       
            }else{
                return View();
            }
        }

        [HttpGet]
        public IActionResult BuscarCadete(int id)
        {   
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }

            Cadete cad = _repository.FindById(id);
            CadeteViewModel _cadVM = _mapper.Map<CadeteViewModel>(cad);
            return View(_cadVM);
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
