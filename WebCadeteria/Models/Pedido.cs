namespace WebCadeteria.Models{
    public class Pedido{
        private int nro;
        private string obs;
        private string estado;

        //El pedido ahora no tiene un objeto Cliente sino su ID
        private int cliente;
        private int cadete;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public string Estado { get => estado; set => estado = value; }
        public int Cliente { get => cliente; set => cliente = value; }
        public int Cadete { get => cadete; set => cadete = value; }

        public Pedido(){

        }
        public Pedido(int _nro, string _obs, string _estado, int _id_cli, int _id_cad){
            Nro = _nro;
            Obs = _obs;
            Estado = _estado;
            Cliente = _id_cli;
            Cadete = _id_cad;
        }
    }
}
