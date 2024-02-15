using System.ComponentModel.DataAnnotations;

namespace WebCadeteria.ViewModels{
    public class CargarCadeteViewModel{

        [Required]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [Required, StringLength(100)]
        public string Direccion { get; set; }

        [Required, Range(0,999999999)]
        public long Telefono { get; set; }

        public CargarCadeteViewModel(){}

        public CargarCadeteViewModel(int id, string _nombre, string _direccion, long _telefono){
            Id = id;
            Nombre = _nombre;
            Direccion = _direccion;
            Telefono = _telefono;
        }
    }
}
