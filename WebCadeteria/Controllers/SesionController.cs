using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using Microsoft.AspNetCore.Http;
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

        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {  
            return View();
        }

        [HttpPost]
        public ActionResult Login(SesionViewModel _sesionVM) 
        {
            if(ModelState.IsValid){
                if (string.IsNullOrEmpty(_sesionVM.User) || string.IsNullOrEmpty(_sesionVM.Password)){
                    return View("Login");
                }else{
                    try
                    {
                        using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                        
                            string queryString = "SELECT * FROM Usuarios WHERE Usuario = @usuario AND Contraseña = @contra ";
                                        
                            var command = new SqliteCommand(queryString, connection);
                            
                            command.Parameters.AddWithValue("@usuario", _sesionVM.User);
                            command.Parameters.AddWithValue("@contra", _sesionVM.Password);

                            connection.Open();
                            
                            using(SqliteDataReader reader = command.ExecuteReader()){

                                if(reader.HasRows){ // Hubo una coincidencia en usuario y contraseña
                                    while(reader.Read()){
                                        
                                        HttpContext.Session.SetInt32("Id", reader.GetInt32(0));
                                        HttpContext.Session.SetString("Nombre", reader.GetString(1));
                                        HttpContext.Session.SetString("Usuario", reader.GetString(2));
                                        HttpContext.Session.SetString("Contraseña", reader.GetString(3));
                                        HttpContext.Session.SetString("Rol", reader.GetString(4));
                                        if (HttpContext.Session.GetString("Rol") == "Cadete"){
                                            HttpContext.Session.SetInt32("IdCad", reader.GetInt32(5));
                                        }else{
                                            HttpContext.Session.SetInt32("IdCad", 99999); //de todas formas no se usa ese campo para los controles de admin
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
                return View("../Home/Opciones");
            }else{
                return View();
            }     
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
