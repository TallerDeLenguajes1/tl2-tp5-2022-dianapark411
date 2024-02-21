using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace WebCadeteria.Controllers{
    public class CadeteController : Controller
    {       
        private readonly ILogger<CadeteController> _logger;
        private readonly IMapper _mapper;
        private readonly ICadete _repository;

        public CadeteController(ILogger<CadeteController> logger, IMapper mapper, ICadete repository)
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
                    return View("ListarCadetes", _repository.FindAll());
                }else{
                    //TempData ["Message"] = "No cuenta con los permisos necesarios para realizar esta acción, comuníquese con el administrador";
                    //return RedirectToAction("Index", "Home");
                    return View("../Sesion/ErrorPermisos");
                }
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult CargarCadete()
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
        public IActionResult CargarCadete(CargarCadeteViewModel _cadeteVM)
        {
            try{
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
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }   
        }

        [HttpPost]
        public IActionResult BajarCadete(int id){
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
        public IActionResult ModificarCadete(int id)
        {   
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }
                
                if(es_admin()){
                    Cadete cad = _repository.FindById(id);
                    ModificarCadeteViewModel _cadeteVM = _mapper.Map<ModificarCadeteViewModel>(cad);
                    return View(_cadeteVM);
                }else{
                    return View("../Sesion/ErrorPermisos");
                }
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }  
        }

        [HttpPost]
        public IActionResult ModificarCadete(ModificarCadeteViewModel _cadeteVM)
        {
            try{
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
            }catch (Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }   
        }

        
        [HttpGet]
        public IActionResult MostrarCadetes(){
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
        public IActionResult BuscarCadete(int id)
        {   
            try{
                if (!esta_logueado()) {
                    return RedirectToRoute(new { controller = "Sesion", action = "Index" });
                }

                Cadete cad = _repository.FindById(id);
                CadeteViewModel _cadVM = _mapper.Map<CadeteViewModel>(cad);
                return View(_cadVM);
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
