using WebCadeteria.Models;
using WebCadeteria.ViewModels;
public interface IPedido
{
    Pedido FindById(int id);
    List<PedidoViewModel> FindAll();
    void Insert(Pedido ped);
    void Update(Pedido ped);
    void Delete(int id);
}
   