using SubastaWeb.Models.Producto;
using SubastaWeb.Models.Usuario;
using System.Timers;

namespace SubastaWeb.Models.Subasta
{
    public class SubastaCerrada : IStrategySubasta
    {
        private decimal oferta;
        private DateTime ultimaOferta;
        private System.Timers.Timer timer;

        public bool Activa { get; private set; }
        public void CerrarSubasta()
        {
            if (Activa)
            {
                Activa = false;
                timer.Stop();
                Console.WriteLine("Subasta cerrada automáticamente después de 5 minutos.");
                // Lógica para adjudicar el producto o manejar el cierre
            }
        }

        public void IniciarSubasta()
        {
            // Configurar el temporizador para 5 minutos
            timer = new System.Timers.Timer(300000);
            timer.Elapsed += OnTiempoTerminado;
            timer.AutoReset = false; // Solo se ejecuta una vez
            Activa = true;
            timer.Start();
            Console.WriteLine("Subasta iniciada. Se cerrará en 5 minutos.");
        }

        public void RealizarOferta(ProductoModelDBO producto, decimal oferta, UsuarioModelDBO usuario)
        {
            var ofertaExistente = producto.Ofertas.FirstOrDefault(o => o.Id == usuario.Id);

            if (ofertaExistente != null)
            {
                throw new InvalidOperationException("El usuario ya ha realizado una oferta para este producto.");
            }

            var nuevaOferta = new UsuarioModelDBO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                TipoUsuario = usuario.TipoUsuario,
                oferta = (double)oferta
            };

            producto.Ofertas.Add(nuevaOferta);
        }


        private void OnTiempoTerminado(object source, ElapsedEventArgs e)
        {
            CerrarSubasta();
        }
    }

}
