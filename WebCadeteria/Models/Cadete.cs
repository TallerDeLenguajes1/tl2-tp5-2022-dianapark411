namespace WebCadeteria.Models{
    public class Cadete: Persona{
        private List<Pedido> listadoPedidos;

        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        public Cadete(){}

        public Cadete(int _id, string _nombre, string _direccion, int _telefono, List<Pedido> _pedidos):base(_id, _nombre, _direccion, _telefono){
            foreach (var item in _pedidos)
            {
                ListadoPedidos.Add(new Pedido(item));
            }
        }

        public Cadete(int _id, string _nombre, string _direccion, int _telefono):base(_id, _nombre, _direccion, _telefono){}

        public double jornal_a_cobrar(){
            double total = 0;
            for (int i = 0; i < ListadoPedidos.Count(); i++)
            {
                total = total + 300 * Convert.ToInt32(ListadoPedidos[i].Estado);
            }
            return total;
        }

        public void mostrarCadetes(){
            Console.WriteLine("INFORMACION DEL CADETE: ");
            this.mostrarPersona();
            foreach (var item in ListadoPedidos)
            {
                item.mostrarPedido();
            }
        }
    }
}
