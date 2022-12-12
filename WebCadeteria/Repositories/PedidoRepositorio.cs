using WebCadeteria.Models;
using WebCadeteria.ViewModels;
using Microsoft.Data.Sqlite;

namespace WebCadeteria.Repositories{
    public class PedidoRepositorio: IPedido
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public PedidoRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public Pedido FindById(int id){
            Pedido ped = null;
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                
                    string queryString = "SELECT * FROM Pedido WHERE Nro = @id;";
                    var command = new SqliteCommand(queryString, connection);
                    
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    using(SqliteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            ped = new Pedido(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),reader.GetInt32(3), reader.GetInt32(4));
                        }
                    }
                    connection.Close();
                }
            }catch{
                throw;
            }
            return ped;
        }
        public List<PedidoViewModel> FindAll(){
            List<PedidoViewModel> ListadoPedidosVM = new();
            string queryString = "SELECT * FROM Pedido;";
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();
                    using(SqliteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            ListadoPedidosVM.Add( new PedidoViewModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),reader.GetInt32(3), reader.GetInt32(4)));
                        }
                    }
                    connection.Close();
                }
            }catch{   
                throw;
            }

            return ListadoPedidosVM;
        }

        public void Insert(Pedido ped){
            string queryString = "INSERT INTO Pedido (Obs, Estado, Cliente, Cadete) values (@_obs, @_estado, @_cliente, @_cadete);";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_obs", ped.Obs);
                    command.Parameters.AddWithValue("@_estado", ped.Estado);
                    command.Parameters.AddWithValue("@_cliente", ped.Cliente);
                    command.Parameters.AddWithValue("@_cadete", ped.Cadete);
                    
                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch{
                throw;
            }
        }

        public void Update(Pedido ped){
            string queryString = "UPDATE Pedido set Obs = @_obs, Estado = @_estado, Cliente = @_cliente, Cadete = @_cadete WHERE Nro = @_id;";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_id", ped.Nro);
                    command.Parameters.AddWithValue("@_obs", ped.Obs);
                    command.Parameters.AddWithValue("@_estado", ped.Estado);
                    command.Parameters.AddWithValue("@_cliente", ped.Cliente);
                    command.Parameters.AddWithValue("@_cadete", ped.Cadete);

                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch{
                throw;
            }
        }

        public void Delete(int id){
            string queryString = "DELETE FROM Pedido WHERE Nro = @_id;";
            try{
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    var command = new SqliteCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@_id",id);
                   
                    command.ExecuteNonQuery();
                    connection.Close();
                }       
            }catch{
                throw;
            }
        }


        public List<PedidoViewModel> FindByIdCadete(int id_cad){
            List<PedidoViewModel> ListadoPedidosVM = new();
            string queryString = "SELECT * FROM Pedido WHERE Cadete = @cadete;";
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_connectionString)){
                    
                    var command = new SqliteCommand(queryString, connection);
                    command.Parameters.AddWithValue("@cadete",id_cad);
                    connection.Open();
                    using(SqliteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            ListadoPedidosVM.Add( new PedidoViewModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),reader.GetInt32(3), reader.GetInt32(4)));
                        }
                    }
                    connection.Close();
                }
            }catch{   
                throw;
            }

            return ListadoPedidosVM;
        }
    }
}