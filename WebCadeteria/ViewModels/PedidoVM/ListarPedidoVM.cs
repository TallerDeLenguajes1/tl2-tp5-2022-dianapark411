using System.ComponentModel.DataAnnotations;

namespace WebCadeteria.ViewModels{
    public class ListarPedidoViewModel{

        [Required, Range(0, int.MaxValue)] //nro positivo
        public int Nro { get; set; }

        [Required, StringLength(200)]
        public string Obs { get; set; }

        [Required, StringLength(100)]
        public string Estado { get; set; }

        [Required, StringLength(100)]   //aqui cambio el tipo de dato de Cliente para mostrar su nombre
        public string Cliente { get; set; }

        [Required, StringLength(100)]   //aqui cambio el tipo de dato de Cadete para mostrar su nombre
        public string Cadete { get; set; }

        public ListarPedidoViewModel(){}

        public ListarPedidoViewModel(int _nro, string _obs, string _estado, string _cliente, string _cadete){
            Nro = _nro;
            Obs = _obs;
            Estado = _estado;
            Cliente = _cliente;
            Cadete = _cadete;
        }
    }
}
