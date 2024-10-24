using SubastaWeb.Models.Producto;
using SubastaWeb.Models.Usuario;

namespace SubastaWeb.Models.Subasta
{
    public interface IStrategySubasta
    {
        void IniciarSubasta();
        void CerrarSubasta();
        void RealizarOferta(ProductoModelDBO producto, decimal oferta, UsuarioModelDBO usuario);
        bool Activa { get; }
    }
}