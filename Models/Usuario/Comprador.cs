using Microsoft.EntityFrameworkCore;
using SubastaWeb.Models.Producto;
using System.Web.Mvc;

namespace SubastaWeb.Models.Usuario
{
    public class Comprador : Usuario
    {
        public override bool RealizarOferta(decimal monto, int id, ProductoViewModel viewModel)
        {
            throw new NotImplementedException();
        }
        private bool NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
