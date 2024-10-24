
using SubastaWeb.Models.Producto;

namespace SubastaWeb.Models.Usuario
{
    public abstract class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Método abstracto a implementar solo por los compradores
        public abstract bool RealizarOferta(decimal monto, int id, ProductoViewModel viewModel);
    }
}
