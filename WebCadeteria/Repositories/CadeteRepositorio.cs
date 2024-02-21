using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using Microsoft.Data.Sqlite;
using NLog;

namespace WebCadeteria.Repositories{
    public class CadeteRepositorio: ICadete
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public CadeteRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public Cadete FindById(int id){
            Cadete cad = null;
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                
                    string queryString = "SELECT * FROM Cadete WHERE Id_cad = @id;";
                    var command = new SqliteCommand(queryString, connection);
                    
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    using(SqliteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            cad = new Cadete(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToInt64(reader.GetString(3)));

                            //Cuando agregue el el id_cadeteria a cadete:
                            //cad = new Cadete(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToInt64(reader.GetString(3)), reader.GetInt32(4));
                        }
                    }

                    connection.Close();
                }
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al buscar el cadete. ");
            }
            return cad;
        }
        public List<ListarCadeteViewModel> FindAll(){
            List<ListarCadeteViewModel> ListadoCadetesVM = new();
            string queryString = "SELECT * FROM Cadete;";
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();
                    using(SqliteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            ListadoCadetesVM.Add( new ListarCadeteViewModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToInt64(reader.GetString(3))));
                        }
                    }
                    connection.Close();
                }
            }catch(Exception ex){   
                Logger.Error(ex.Message,"Error al buscar los cadetes.");
            }

            return ListadoCadetesVM;
        }

        public void Insert(Cadete cad){
            string queryString = "INSERT INTO Cadete (Nombre, Direccion, Telefono) values (@_nombre, @_direccion, @_telefono);";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_nombre", cad.Nombre);
                    command.Parameters.AddWithValue("@_direccion", cad.Direccion);
                    command.Parameters.AddWithValue("@_telefono", cad.Telefono);

                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al insertar el cadete.");
            }
        }

        public void Update(Cadete cad){
            string queryString = "UPDATE Cadete set Nombre = @_nombre, Direccion = @_direccion, Telefono = @_telefono WHERE Id_cad = @_id;";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_id", cad.Id);
                    command.Parameters.AddWithValue("@_nombre", cad.Nombre);
                    command.Parameters.AddWithValue("@_direccion", cad.Direccion);
                    command.Parameters.AddWithValue("@_telefono", cad.Telefono);

                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al actualizar el cadete.");
            }
        }

        public void Delete(int id){
            string queryString = "DELETE FROM Cadete WHERE Id_cad = @_id;";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_id",id);
                   
                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch(Exception ex){
                Logger.Error(ex.Message, "Error al eliminar el cadete.");
            }
        }
    }
}