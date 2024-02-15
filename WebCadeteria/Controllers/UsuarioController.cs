using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using AutoMapper;

namespace WebCadeteria.Controllers{
    public class UsuarioController : Controller
    {   
        private readonly ILogger<HomeController> _logger;
        
        private readonly IMapper _mapper;
        private readonly IUsuario _repository;

        public UsuarioController(ILogger<HomeController> logger, IMapper mapper, IUsuario repository)
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
                return View("ListarUsuarios", _repository.FindAll());
            }else{
                return View("../Sesion/ErrorPermisos");
            } 
        }

        [HttpGet]
        public IActionResult CargarUsuario()
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
        public IActionResult CargarUsuario(CargarUsuarioViewModel _usuarioVM){
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            if(ModelState.IsValid){   
                if(es_admin()){
                    Usuario user = _mapper.Map<Usuario>(_usuarioVM);
                    _repository.Insert(user);

                    return RedirectToAction("Index");
                }else{
                    return View("../Sesion/ErrorPermisos");
                }      
            }else{
                return View();
            }
        }


        public IActionResult MostrarUsuarios(){
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }

            if(es_admin()){
                return RedirectToAction("Index"); //para que index pase el viewmodel
            }else{
                return View("../Sesion/ErrorPermisos");
            }
        }

        public IActionResult BajarUsuario(int id){
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
        public IActionResult ModificarUsuario(int id)
        {   
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            
            if(es_admin()){
                Usuario user = _repository.FindById(id);
                ModificarUsuarioViewModel _usuarioVM = _mapper.Map<ModificarUsuarioViewModel>(user);
                return View(_usuarioVM);
            }else{
                return View("../Sesion/ErrorPermisos");
            }               
        }

        [HttpPost]
        public IActionResult ModificarUsuario(ModificarUsuarioViewModel _usuarioVM) 
        {   
            if (!esta_logueado()) {
                return RedirectToRoute(new { controller = "Sesion", action = "Index" });
            }
            if(ModelState.IsValid){
                if(es_admin()){
                    Usuario user = _mapper.Map<Usuario>(_usuarioVM);
                    _repository.Update(user);

                    return RedirectToAction("Index");
                }else{
                    return View("../Sesion/ErrorPermisos");
                }   
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
