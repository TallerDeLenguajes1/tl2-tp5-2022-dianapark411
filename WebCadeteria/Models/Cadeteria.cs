namespace WebCadeteria.Models{
    public class Cadeteria{
        private string nombre;
        private long telefono_cadeteria;
        private List<Cadete> listadoCadetes;
        private List<Pedido> listadoPedidos;
        private List<Cliente> listadoClientes;

        public string Nombre { get => nombre; set => nombre = value; }
        public long Telefono_cadeteria { get => telefono_cadeteria; set => telefono_cadeteria = value; }
        public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }
        public List<Cliente> ListadoClientes { get => listadoClientes; set => listadoClientes = value; }

        private Helper _helper = new Helper();
        private string path_cadetes = @"Recursos\cadetes.csv";    
        private string path_clientes = @"Recursos\clientes.csv";
        private string path_pedidos = @"Recursos\pedidos.csv";

        public Cadeteria(){
            Nombre = "CADETERIA WEB";
            Telefono_cadeteria = 4350000;

            ListadoCadetes = new List<Cadete>();
            ListadoPedidos= new List<Pedido>();
            ListadoClientes= new List<Cliente>();

        }

    }

}
