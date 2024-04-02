using PetPalApp.Data;
using PetPalApp.Domain;

namespace PetPalApp.Business;

public class SupplierService : ISupplierService
{
  
  private IRepositoryGeneric<Supplier> Srepository;
  private IRepositoryGeneric<User> Urepository;

  public SupplierService(IRepositoryGeneric<Supplier> _srepository, IRepositoryGeneric<User> _urepository)
  {
    Srepository = _srepository;
    Urepository = _urepository;
  }

  public void RegisterService(int idUser, String nameUser, String type, String nameService, string description, decimal price, bool online)
  {
    Supplier service = new(type, nameService, description, price, online);
    AssignId(service);
    service.UserId = idUser;
    Srepository.AddEntity(service);
    var user = Urepository.GetByStringEntity(nameUser);
    user.ListSupplier.Add(service.SupplierId, service);
    Urepository.UpdateEntity(nameUser, user);
  }

  private void AssignId(Supplier service)
  {
    var allServices = Srepository.GetAllEntities();
    int nextId = 0;
    int newId;
    if (allServices == null || allServices.Count == 0)
    {
      service.SupplierId = "1";
    }
    else
    {
      foreach (var item in allServices)
      {
        if (int.Parse(item.Value.SupplierId) > nextId)
        {
        nextId = int.Parse(item.Value.SupplierId);
        }
      }
      newId = nextId + 1;
      service.SupplierId = newId.ToString();
    }
  }

  public string PrintServices(Dictionary<string, Supplier> services)
  {
    String allDataService = "";
    foreach (var item in services)
    {
      string online;
      if (item.Value.SupplierOnline) online = "Yes";
      else online = "No";
      String addService = @$"

    ====================================================================================
    
    - Type:                         {item.Value.SupplierType}
    - Name:                         {item.Value.SupplierName}
    - Desciption:                   {item.Value.SupplierDescription}
    - Date of availabilility:       {item.Value.SupplierAvailability}
    - Home delivery service:        {online}
    - Score:                        {item.Value.SupplierRating}
    - Price:                        {item.Value.SupplierPrice} €
    
    ====================================================================================";

      allDataService += addService;
    }
    return allDataService;
  }

  public Dictionary<string, Supplier> SearchService(string serviceType)
  {
    var allServices = Srepository.GetAllEntities();
    Dictionary<string, Supplier> typeServices = new();
    foreach (var item in allServices)
    {
      if (item.Value.SupplierType.IndexOf(serviceType,StringComparison.OrdinalIgnoreCase) >= 0 || item.Value.SupplierDescription.IndexOf(serviceType, StringComparison.OrdinalIgnoreCase) >= 0 || item.Value.SupplierName.IndexOf(serviceType, StringComparison.OrdinalIgnoreCase) >= 0)
      {
        typeServices.Add(item.Value.SupplierId, item.Value);
      }
    }
    return typeServices;
  }

  public Dictionary<string, Supplier> GetAllServices()
  {
    var allServices = Srepository.GetAllEntities();
    
    return allServices;
  }

  public Dictionary<string, Supplier> ShowMyServices(String key)
  {
    Dictionary<string, Supplier> userServices = Urepository.GetByStringEntity(key).ListSupplier;

    return userServices;
  }

  public void DeleteService(string userName, string serviceId)
  {
    var service = Srepository.GetByStringEntity(serviceId);
    User user = Urepository.GetByStringEntity(userName);
    if (user.ListSupplier.ContainsKey(serviceId))
    {
      Srepository.DeleteEntity(service);
    }
    else Console.WriteLine("The service you want to delete does not exist or belongs to another user.");
  }
}