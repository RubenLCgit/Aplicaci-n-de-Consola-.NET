namespace PetPalApp.Domain;

public class Supplier
{
  public string SupplierId { get; set; }//Se cambia de int a string para poder reutilizar el metodo "GetAllEntities" del repositorio generico.
  public int UserId { get; set; }
  public string SupplierType{ get; set; }
  public string SupplierName { get; set; }
  public string SupplierDescription { get; set; }
  public decimal SupplierPrice { get; set; }
  public DateTime SupplierAvailability { get; set; }
  public bool SupplierOnline { get; set; }
  public double SupplierRating { get; set; }


  public Supplier() { }

  public Supplier(String type, String name, String description, decimal price, bool online)
  {
    SupplierType = type;
    SupplierName = name;
    SupplierDescription = description;
    SupplierPrice = price;
    SupplierOnline = online;
    SupplierAvailability = DateTime.Now;
  }
}