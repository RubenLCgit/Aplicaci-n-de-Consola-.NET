using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface ISupplierService
{
  void RegisterService(int idUser, String nameUser, String type, String nameSupplier, String description, decimal price, bool online);
  Dictionary<string, Supplier> SearchService(string supplierType);

  public Dictionary<string, Supplier> GetAllServices();

  Dictionary<string, Supplier> ShowMyServices(String key);

  public string PrintServices(Dictionary<string, Supplier> suppliers);

  void DeleteService(string userName, string supplierId);
}