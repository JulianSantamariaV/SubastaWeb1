using SubastaWeb.Models.Producto;

namespace SubastaWeb.Servicio.Factory
{
    public class ProductoFactory
    {
        public static Producto CrearProducto(string categoria)
        {
            switch (categoria.ToLower())
            {
                case "alimento":
                    return new ProductoAlimento();
                case "ropa":
                    return new ProductoRopa();
                case "electronica":
                    return new ProductoElectronica();
                default:
                    throw new ArgumentException("Tipo de producto no soportado");
            }
        }
    }
}