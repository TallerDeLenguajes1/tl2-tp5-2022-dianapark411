using WebCadeteria.Models;
using WebCadeteria.ViewModels;
public interface IUsuario
{
    Usuario FindById(int id);
    List<ListarUsuarioViewModel> FindAll();
    void Insert(Usuario user);
    void Update(Usuario user);
    void Delete(int id);

}
   