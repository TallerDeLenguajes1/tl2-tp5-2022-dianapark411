using WebCadeteria.Models;
using WebCadeteria.ViewModels;
public interface ICliente
{
    Cliente FindById(int id);
    List<ListarClienteViewModel> FindAll();
    void Insert(Cliente cli);
    void Update(Cliente cli);
    void Delete(int id);
}
    