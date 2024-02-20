using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using AutoMapper;


namespace WebCadeteria.Controllers{
    public class SesionController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string _connectionString;


        public SesionController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }
        
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {  
            return View(new SesionViewModel());
        }

        [HttpPost]
        public IActionResult Login(SesionViewModel _sesionVM) 
        {
            if(ModelState.IsValid){
                if (string.IsNullOrEmpty(_sesionVM.User) || string.IsNullOrEmpty(_sesionVM.Password)){
                    return RedirectToAction("Index");
                }else{
                    try
                    {
                        using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                        
                            string queryString = "SELECT * FROM Usuarios WHERE Usuario = @_usuario AND Contraseña = @_contra ";
                                        
                            var command = new SqliteCommand(queryString, connection);
                            
                            command.Parameters.AddWithValue("@_usuario", _sesionVM.User);
                            command.Parameters.AddWithValue("@_contra", _sesionVM.Password);

                            connection.Open();
                            
                            using(SqliteDataReader reader = command.ExecuteReader()){

                                if(reader.HasRows){ // Hubo una coincidencia en usuario y contraseña
                                    while(reader.Read()){
                                        
                                        HttpContext.Session.SetInt32("Id", reader.GetInt32(0));
                                        HttpContext.Session.SetString("Nombre", reader.GetString(1));
                                        HttpContext.Session.SetString("Usuario", reader.GetString(2));
                                        HttpContext.Session.SetString("Contraseña", reader.GetString(3));
                                        HttpContext.Session.SetString("Rol", reader.GetString(4));
                                        if(HttpContext.Session.GetString("Rol")=="Cadete")
                                        {
                                            if(!reader.IsDBNull(5)){ // El usuario está asignado a un cadete
                                                HttpContext.Session.SetInt32("Id_cadete", reader.GetInt32(5));
                                            }else{
                                                HttpContext.Session.SetInt32("Id_cadete", -999);
                                            }
                                        }else{
                                            HttpContext.Session.SetInt32("Id_cadete", -999);
                                        }                                
                                    }
                                }else{   
                                    return View("Reintentar");
                                }       
                            }
                            connection.Close();
                        }
                    }catch{
                        throw;
                    }
                    
                }
                return View("../Home/Index");
            }else{
                return View();
            }     
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
