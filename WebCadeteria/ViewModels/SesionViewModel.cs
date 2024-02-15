using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCadeteria.ViewModels{
    public class SesionViewModel{

        [Required]
        public string User { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public SesionViewModel(){}

        public SesionViewModel(string _user, string _password){
            User = _user;
            Password = _password;
        }
    }
}