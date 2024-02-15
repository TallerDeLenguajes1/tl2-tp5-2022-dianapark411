using System.ComponentModel.DataAnnotations;
using WebCadeteria.Models;

namespace WebCadeteria.ViewModels{
    public class ModificarUsuarioViewModel{

        [Required]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [Required, StringLength(30)]
        public string User { get; set; }

        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 5)]
        public string Passwd { get; set; }

        [Required, StringLength(30)]
        public string Rol { get; set; }


        public ModificarUsuarioViewModel(){}

        public ModificarUsuarioViewModel(int _id, string _nombre, string _usuario, string _passwd, string _rol){
            Id = _id;
            Nombre = _nombre;
            User = _usuario;
            Passwd = _passwd;
            Rol = _rol;
        }
    }
}
