using System.ComponentModel.DataAnnotations;

namespace WebCadeteria.ViewModels{
    public class AgregarPedidoViewModel{

        [Required, Range(0, int.MaxValue)] //nro positivo
        public int Nro { get; set; }

        [Required, StringLength(200)]
        public string Obs { get; set; }

        [Required, StringLength(100)]
        public string Estado { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Cliente { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Cadete { get; set; }

        public AgregarPedidoViewModel(){}

        public AgregarPedidoViewModel(int _nro, string _obs, string _estado, int _cliente, int _cadete){
            Nro = _nro;
            Obs = _obs;
            Estado = _estado;
            Cliente = _cliente;
            Cadete = _cadete;
        }
    }
}
