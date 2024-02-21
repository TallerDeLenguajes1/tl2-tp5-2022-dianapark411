using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using Microsoft.Data.Sqlite;
using NLog;

namespace WebCadeteria.Repositories{
    public class ClienteRepositorio: ICliente
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public ClienteRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public Cliente FindById(int id){
            Cliente cli = null;
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                
                    string queryString = "SELECT * FROM Cliente WHERE Id_cli = @id;";
                    var command = new SqliteCommand(queryString, connection);
                    
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    using(SqliteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            cli = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToInt64(reader.GetString(3)), reader.GetString(4));
                        }
                    }
                    connection.Close();
                }
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al buscar el cliente.");
            }
            return cli;
        }
        public List<ClienteViewModel> FindAll(){
            List<ClienteViewModel> ListadoClientesVM = new();
            string queryString = "SELECT * FROM Cliente;";
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();
                    using(SqliteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            ListadoClientesVM.Add( new ClienteViewModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToInt64(reader.GetString(3)), reader.GetString(4)));
                        }
                    }
                    connection.Close();
                }
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al buscar los clientes.");
            }

            return ListadoClientesVM;
        }

        public void Insert(Cliente cli){
            string queryString = "INSERT INTO Cliente (Nombre, Direccion, Telefono, DatosRefDir) values (@_nombre, @_direccion, @_telefono, @_ref_dir);";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_nombre", cli.Nombre);
                    command.Parameters.AddWithValue("@_direccion", cli.Direccion);
                    command.Parameters.AddWithValue("@_telefono", cli.Telefono);
                    command.Parameters.AddWithValue("@_ref_dir", cli.DatosReferenciaDireccion);
                    
                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al insertar el cliente.");
            }
        }

        public void Update(Cliente cli){
            string queryString = "UPDATE Cliente set Nombre = @_nombre, Direccion = @_direccion, Telefono = @_telefono, DatosRefDir = @_ref_dir WHERE Id_cli = @_id;";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_id", cli.Id);
                    command.Parameters.AddWithValue("@_nombre", cli.Nombre);
                    command.Parameters.AddWithValue("@_direccion", cli.Direccion);
                    command.Parameters.AddWithValue("@_telefono", cli.Telefono);
                    command.Parameters.AddWithValue("@_ref_dir", cli.DatosReferenciaDireccion);

                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al actualizar el cliente.");
            }
        }

        public void Delete(int id){
            string queryString = "DELETE FROM Cliente WHERE Id_cli = @_id;";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_id",id);
                   
                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al eliminar el cliente.");
            }
        }
    }
}