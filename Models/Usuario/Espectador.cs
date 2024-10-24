using SubastaWeb.Models.Producto;

namespace SubastaWeb.Models.Usuario
{
    public class Espectador : Usuario
    {
        public override bool RealizarOferta(decimal monto, int id, ProductoViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
