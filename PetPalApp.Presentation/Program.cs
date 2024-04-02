using PetPalApp.Business;
using PetPalApp.Domain;
using PetPalApp.Data;
using PetPalApp.Presentation;

//AQUI SE PRODUCE LA INYECCIÓN DE DEPENDENCIAS
//ES DONDE INYECTAMOS A LAS INSTANCIAS DE LAS INTERFACES LA CLASE QUE IMPLEMENTARÁ SUS METODOS.
IRepositoryGeneric<User> userRepository= new UserRepository();
IRepositoryGeneric<Supplier> supplierRepository= new SupplierRepository();
IRepositoryGeneric<Product> productRepository= new ProductRepository();
IUserService userService = new UserService(userRepository);
ISupplierService supplierService = new SupplierService(supplierRepository, userRepository);
IProductService productService = new ProductService(productRepository, userRepository);
MainMenu mainMenu = new(userService, supplierService, productService);
mainMenu.ApplicationInit();