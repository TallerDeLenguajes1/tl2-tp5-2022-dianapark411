using System.ComponentModel.DataAnnotations;

namespace WebCadeteria.ViewModels{
    public class PedidoViewModel{

        [Required]
        public int Nro { get; set; }

        [Required, StringLength(200)]
        public string Obs { get; set; }

        [Required, StringLength(100)]
        public string Estado { get; set; }

        [Required]
        public int Cliente { get; set; }

        [Required]
        public int Cadete { get; set; }

        public PedidoViewModel(){}

        public PedidoViewModel(int nro, string _obs, string _estado, int _cliente, int _cadete){
            Nro = nro;
            Obs = _obs;
            Estado = _estado;
            Cliente = _cliente;
            Cadete = _cadete;
        }
    }
}
