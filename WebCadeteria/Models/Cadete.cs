namespace WebCadeteria.Models{
    public class Cadete: Persona{
        //private int cadeteria;

        private List<Pedido> listadoPedidos;

        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }
        //public int Cadeteria { get => cadeteria; set => cadeteria = value; }

        public Cadete(){}

        public Cadete(int _id, string _nombre, string _direccion, long _telefono):base(_id, _nombre, _direccion, _telefono){}
    }
}
