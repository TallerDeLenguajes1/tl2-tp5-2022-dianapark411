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

            _helper.leerCadetes(path_cadetes, ListadoCadetes);
            _helper.leerClientes(path_clientes, ListadoClientes);

            StreamReader file = new StreamReader(path_pedidos);
            string line = "";
            //Lee linea por linea hasta que termina el archivo
            while ((line = file.ReadLine()) != null)
            {
                string[] Fila = line.Split(';');
                Cliente cli;
                bool encontrado;
                (cli,encontrado) = buscarCliente(Fila[3]);
                if(encontrado){
                    ListadoPedidos.Add(new Pedido(Convert.ToInt32(Fila[0]), Fila[1], Fila[2], cli));
                }
                // agregar un else que si no lo encuentre mande a cargar cliente           
            }
            file.Close();
        }

        public (Cliente, bool) buscarCliente(string _telefono){

            int id = 99999;
            bool encontrado = false;
            for (int i = 0; i < listadoClientes.Count(); i++){
                if (listadoClientes[i].Telefono == Convert.ToInt64(_telefono)){
                    id = i;
                    encontrado = true;
                }
            }

            return (listadoClientes[id], encontrado);
        }


        public void mostrarCadeteria(){
            Console.WriteLine("INFORMACION DE LA CADETERIA: ");
            Console.WriteLine("NOMBRE: ", Nombre);
            Console.WriteLine("TELEFONO: ", Telefono_cadeteria);
            foreach (var item in ListadoCadetes)
            {
                item.mostrarCadetes();
            }
        }
    }

}
