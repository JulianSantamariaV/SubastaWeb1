namespace SubastaWeb.Models.Producto
{
    public class ProductoElectronica : Producto
    {
        public override void CalcularPrecioFinal()
        {
            PrecioFinal *= 1.10m;
        }
    }
}