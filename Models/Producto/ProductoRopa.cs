namespace SubastaWeb.Models.Producto
{
    public class ProductoRopa : Producto
    {
        public override void CalcularPrecioFinal()
        {
            if (EnLiquidacion)
            {
                PrecioFinal *= 0.80m;
            }
            else
            {
                PrecioFinal = PrecioFinal;
            }
        }
    }
}