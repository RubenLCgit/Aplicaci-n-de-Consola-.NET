using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class ProductRepository : IRepositoryGeneric<Product>
{
  public Dictionary<string, Product> EntityDictionary = new Dictionary<string, Product>();
  private readonly string _filePath = Path.Combine("PetPalApp", "SharedFolder", "ProductRepository", "ProductRepository.json");
  public void AddEntity(Product entity) //MODIFICADO
  {
    try
    {
      if (File.Exists(_filePath))
      {
        EntityDictionary = GetAllEntities();
      }
      EntityDictionary.Add(entity.ProductId, entity);
      SaveChanges();
    }
    catch (Exception ex)
    {
      throw new Exception("No se ha podido realizar el registro", ex);
    }
  }

  public void DeleteEntity(Product entity)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary.Remove(entity.ProductId);
    SaveChanges();
  }

  public Dictionary<string, Product> GetAllEntities() //MODIFICADO
  {
    String jsonString;
    if (File.Exists(_filePath))
    {
      jsonString = File.ReadAllText(_filePath);
      EntityDictionary = JsonSerializer.Deserialize<Dictionary<string, Product>>(jsonString);
      return EntityDictionary;
    }
    else
    {
      return EntityDictionary;
    }
  }

  public Product GetByStringEntity(string key)
  {
    var dictionaryCurrentProduct = GetAllEntities();
    Product product = new();
    foreach (var item in dictionaryCurrentProduct)
    {
      if (item.Value.ProductId.Equals(key, StringComparison.OrdinalIgnoreCase))
      {
        product = item.Value;
        break;
      }
    }
    return product;
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

  public void UpdateEntity(string key, Product product)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary[key] = product;
    SaveChanges();
  }
}