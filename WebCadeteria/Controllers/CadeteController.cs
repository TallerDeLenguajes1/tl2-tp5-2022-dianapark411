using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using AutoMapper;

namespace WebCadeteria.Controllers{
    public class CadeteController : Controller
    {
        private string path_cadetes = @"Recursos\cadetes.csv";    
        private Cadeteria _cadeteria = new Cadeteria();
        
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        public CadeteController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CargarCadete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CargarCadete(CadeteViewModel _cadeteVM)
        {
            int id_actual = _cadeteria.ListadoCadetes.Count();
            if(ModelState.IsValid){         
                Cadete cad = _mapper.Map<Cadete>(_cadeteVM);
                System.IO.File.AppendAllText(path_cadetes, $"{id_actual};{cad.Nombre};{cad.Direccion};{cad.Telefono}\n");
                _cadeteria.ListadoCadetes.Add(cad);
                return View("ListarCadetes");
            }else{
                return View();
            }
            
        }

        public IActionResult MostrarCadetes(){
            return View("ListarCadetes");
        }

        public IActionResult BajarCadete(int id){
            string[] leer = System.IO.File.ReadAllLines(path_cadetes);
            System.IO.File.WriteAllText(path_cadetes,"");
            
            for (int i = 0; i < leer.Length; i++){
                string[] datos = leer[i].Split(";");
                if (id == Convert.ToInt32(datos[0])){   
                    _cadeteria.ListadoCadetes.RemoveAt(id);

                    for(int j = id-1; j < leer.Length -2; j++){ //length -2 porque ya hice un remove y se resta 1, y porque adentro hago j+1 para que nose se pase del rango
                        System.IO.File.AppendAllText(path_cadetes, $"{j+1};{ _cadeteria.ListadoCadetes[j+1].Nombre};{ _cadeteria.ListadoCadetes[j+1].Direccion};{ _cadeteria.ListadoCadetes[j+1].Telefono}" + "\n");
                    }
                    break;
                }else{
                    System.IO.File.AppendAllText(path_cadetes, leer[i] +"\n");  
                } 
            }

            return View("ListarCadetes");
        }

        [HttpGet]
        public IActionResult ModificarCadete(int id)
        {   
            Cadete cad = _cadeteria.ListadoCadetes[id];
            CadeteViewModel _cadeteVM = _mapper.Map<CadeteViewModel>(cad);
            return View(_cadeteVM);
        }

        [HttpPost]
        public IActionResult ModificarCadete(CadeteViewModel _cadeteVM)
        {
            Cadete cad = _mapper.Map<Cadete>(_cadeteVM);
            _cadeteria.ListadoCadetes[cad.Id] = cad;

            string[] leer = System.IO.File.ReadAllLines(path_cadetes);
            System.IO.File.WriteAllText(path_cadetes,"");
            

            for (int i = 0; i < leer.Length; i++){
                string[] datos = leer[i].Split(";");
                if (cad.Id == Convert.ToInt32(datos[0])){   
                    System.IO.File.AppendAllText(path_cadetes, $"{cad.Id};{cad.Nombre};{cad.Direccion};{ cad.Telefono}" + "\n");
                }else{
                    System.IO.File.AppendAllText(path_cadetes, leer[i] +"\n");  
                } 
            }
            return View("ListarCadetes");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
