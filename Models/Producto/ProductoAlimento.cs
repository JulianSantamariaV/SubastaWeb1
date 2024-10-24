using System.ComponentModel.DataAnnotations;

namespace SubastaWeb.Models.Producto
{
    public class ProductoAlimento : Producto
    {
        public override void CalcularPrecioFinal()
        {
            int diasAntesCaducidad = 7;
            DateTime fechaActual = DateTime.Now;

            if (FechaCaducidad.HasValue)
            {

                if ((FechaCaducidad.Value - fechaActual).TotalDays <= diasAntesCaducidad)
                {
                    PrecioFinal *= 0.85m;
                }
                else
                {
                    PrecioFinal = PrecioFinal;
                }
            }
            else
            {
                PrecioFinal = PrecioFinal;
            }
        }
    }
}