using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class SupplierRepository : IRepositoryGeneric<Supplier>
{

  public Dictionary<string, Supplier> EntityDictionary = new Dictionary<string, Supplier>();
  private readonly string _filePath = Path.Combine("PetPalApp", "SharedFolder", "ServiceRepository", "ServicesRepository.json");
  public void AddEntity(Supplier entity)
  {
    Dictionary<string, Supplier> listServices;
    try
    {
      if (File.Exists(_filePath))
      {
        listServices = GetAllEntities();
        EntityDictionary = listServices;
      }
      EntityDictionary.Add(entity.SupplierId, entity);
      SaveChanges();
    }
    catch (Exception ex)
    {
      throw new Exception("No se ha podido realizar el registro", ex);
    }
  }

  public void DeleteEntity(Supplier entity)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary.Remove(entity.SupplierId);
    SaveChanges();
  }

  public Dictionary<string, Supplier> GetAllEntities()
  {
    Dictionary <string, Supplier> dictionaryUsers = new Dictionary<string, Supplier>();;
    String jsonString;
    if (File.Exists(_filePath))
    {
      jsonString = File.ReadAllText(_filePath);
      dictionaryUsers = JsonSerializer.Deserialize<Dictionary<string, Supplier>>(jsonString);
    }
    else
    {
      dictionaryUsers = EntityDictionary;
    }
    return dictionaryUsers;
  }
  public Supplier GetByStringEntity(string id)
  {
    var dictionaryCurrentService = GetAllEntities();
    Supplier supplier = new();
    foreach (var item in dictionaryCurrentService)
    {
      if (item.Value.SupplierId.Equals(id, StringComparison.OrdinalIgnoreCase))
      {
        supplier = item.Value;
        break;
      }
    }
    return supplier;
  }

  public void SaveChanges()
  {
    string directoryPath = Path.GetDirectoryName(_filePath);
    if (!Directory.Exists(directoryPath))
    {
      Directory.CreateDirectory(directoryPath);
    }
    var serializeOptions = new JsonSerializerOptions { WriteIndented = true };
    string jsonString = JsonSerializer.Serialize(EntityDictionary, serializeOptions);
    File.WriteAllText(_filePath, jsonString);
  }

  public void UpdateEntity(string key, Supplier supplier)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary[key] = supplier;
    SaveChanges();
  }
}