namespace WebCadeteria.Models{
    public class Pedido{
        private int nro;
        private string obs;
        private string estado;
        private Cliente cliente;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public string Estado { get => estado; set => estado = value; }
        internal Cliente Cliente { get => cliente; set => cliente = value; }

        public Pedido(){

        }
        public Pedido(int _nro, string _obs, string _estado, int _id, string _nombre, string _direccion, long _telefono, string _datosReferenciaDireccion){
            Nro = _nro;
            Obs = _obs;
            Estado = _estado;
            Cliente = new Cliente(_id,_nombre,_direccion, _telefono,_datosReferenciaDireccion);
        }
        public Pedido(int _nro, string _obs, string _estado, Cliente _cli){
            Nro = _nro;
            Obs = _obs;
            Estado = _estado;
            Cliente = _cli;
        }

        public Pedido(Pedido p) //constructor copia
        {
            Nro = p.Nro;
            Obs = p.Obs;
            Estado = p.Estado;
            Cliente = new Cliente(p.Cliente.Id, p.Cliente.Nombre, p.Cliente.Direccion, p.Cliente.Telefono, p.Cliente.DatosReferenciaDireccion);
        }

        public void mostrarPedido(){
            Console.WriteLine("INFORMACION DEL PEDIDO ");
            Console.WriteLine($"Numero: {Nro}");
            Console.WriteLine($"Observacion: {Obs}");
            Console.WriteLine($"Estado: {Estado}");
            this.Cliente.mostrarCliente();
        }
    }
}
