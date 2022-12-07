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
            return View("ListarClientes", _repository.FindAll());
        }

        public IActionResult CargarCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CargarCliente(ClienteViewModel _clienteVM)
        {
            if(ModelState.IsValid){         
                Cliente cli = _mapper.Map<Cliente>(_clienteVM);
                _repository.Insert(cli);

                return RedirectToAction("Index");
            }else{
                return View();
            }
        }

        public IActionResult MostrarClientes()
        {
            return RedirectToAction("Index");
        }
        

        public IActionResult BajarCliente(int id){
           
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ModificarCliente(int id)
        {   
            Cliente cli = _repository.FindById(id);
            ClienteViewModel _clienteVM = _mapper.Map<ClienteViewModel>(cli);
            return View(_clienteVM);
        }

        [HttpPost]
        public IActionResult ModificarCliente(ClienteViewModel _clienteVM)
        {
           if(ModelState.IsValid){
                
                Cliente cli = _mapper.Map<Cliente>(_clienteVM);
                _repository.Update(cli);

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
