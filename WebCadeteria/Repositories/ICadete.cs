using WebCadeteria.Models;
using WebCadeteria.ViewModels;
public interface ICadete
{
    Cadete FindById(int id);
    List<CadeteViewModel> FindAll();
    void Insert(Cadete cad);
    void Update(Cadete cad);
    void Delete(int id);
    
}
    