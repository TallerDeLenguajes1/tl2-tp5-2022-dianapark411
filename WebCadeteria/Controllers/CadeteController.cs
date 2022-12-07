using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using AutoMapper;

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
            return View("ListarCadetes", _repository.FindAll());
        }

        public IActionResult CargarCadete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CargarCadete(CadeteViewModel _cadeteVM)
        {
            if(ModelState.IsValid){
                
                Cadete cad = _mapper.Map<Cadete>(_cadeteVM);
                _repository.Insert(cad);

                return RedirectToAction("Index");
            }else{
                return View();
            }  
        }

        public IActionResult MostrarCadetes(){
            return RedirectToAction("Index"); //para que index pase el viewmodel
        }

        public IActionResult BajarCadete(int id){
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ModificarCadete(int id)
        {   
            Cadete cad = _repository.FindById(id);
            CadeteViewModel _cadeteVM = _mapper.Map<CadeteViewModel>(cad);
            return View(_cadeteVM);
        }

        [HttpPost]
        public IActionResult ModificarCadete(CadeteViewModel _cadeteVM)
        {
            if(ModelState.IsValid){
                
                Cadete cad = _mapper.Map<Cadete>(_cadeteVM);
                _repository.Update(cad);

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
