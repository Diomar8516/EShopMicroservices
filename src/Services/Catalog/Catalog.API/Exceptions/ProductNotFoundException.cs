namespace Catalog.API.Exceptions;
public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Producto no encontrado!")
    { 
    }
}
