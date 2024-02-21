using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using Microsoft.Data.Sqlite;
using NLog;

namespace WebCadeteria.Repositories{
    public class UsuarioRepositorio: IUsuario
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public Usuario FindById(int id){
            Usuario user = null;
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                
                    string queryString = "SELECT * FROM Usuarios WHERE Id = @_id;";
                    var command = new SqliteCommand(queryString, connection);
                    
                    command.Parameters.AddWithValue("@_id", id);

                    connection.Open();
                    using(SqliteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            user = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),reader.GetString(3), reader.GetString(4));
                        }
                    }
                    connection.Close();
                }
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al buscar el usuario.");
            }
            return user;
        }

        public List<ListarUsuarioViewModel> FindAll(){
            List<ListarUsuarioViewModel> ListadoUsuariosVM = new();
            string queryString = "SELECT * FROM Usuarios;";
            
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    using(SqliteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                             
                            ListadoUsuariosVM.Add( new ListarUsuarioViewModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                        }
                    }
                    connection.Close();
                }
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al buscar los usuarios.");
            }
            return ListadoUsuariosVM;
        }

        public void Insert(Usuario user){
            string queryString = "INSERT INTO Usuarios (Nombre, Usuario, Contraseña, Rol) values (@_nombre, @_usuario, @_passwd, @_rol);";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_nombre", user.Nombre);
                    command.Parameters.AddWithValue("@_usuario", user.User);
                    command.Parameters.AddWithValue("@_passwd", user.Passwd);
                    command.Parameters.AddWithValue("@_rol", user.Rol);
                    
                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al insertar el usuario.");
            }
        }

        public void Update(Usuario user){
            string queryString = "UPDATE Usuarios set Id = @_id, Nombre = @_nombre, Usuario = @_usuario, Contraseña = @_passwd, Rol = @_rol WHERE Id = @_id;";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_id", user.Id);
                    command.Parameters.AddWithValue("@_nombre", user.Nombre);
                    command.Parameters.AddWithValue("@_usuario", user.User);
                    command.Parameters.AddWithValue("@_passwd", user.Passwd);
                    command.Parameters.AddWithValue("@_rol", user.Rol);

                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al actualizar el usuario.");
            }
        }

        public void Delete(int id){
            string queryString = "DELETE FROM Usuarios WHERE Id = @_id;";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_id", id);
                   
                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al eliminar el usuario.");
            }
        }


        // Función para convertir el valor entero del enum a su representación en texto
        // No usada ya que se eliminó el enum
        /*
        private string GetRolString(int rolInt) {
            switch (rolInt) {
                case 0:
                    return Rol.administrador.ToString();
                case 1:
                    return Rol.cadete.ToString();
                default:
                    throw new ArgumentException("Valor de rol no válido");
            }
        }
        */

    }
}