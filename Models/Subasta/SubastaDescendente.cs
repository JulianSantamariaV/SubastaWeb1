using SubastaWeb.Models.Producto;
using SubastaWeb.Models.Usuario;
using System.Timers;

namespace SubastaWeb.Models.Subasta
{
    public class SubastaDescendente : IStrategySubasta
    {
        private decimal ofertaAnterior;
        private DateTime ultimaOferta;
        private System.Timers.Timer timer;

        public bool Activa { get; private set; }
        public SubastaDescendente()
        {
            ofertaAnterior = 0;
            Activa = false;
        }      
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

        private void OnTiempoTerminado(object source, ElapsedEventArgs e)
        {
            CerrarSubasta();
        }

        public void RealizarOferta(decimal oferta)
        {
            throw new NotImplementedException();
        }

        public void RealizarOferta(ProductoModelDBO producto, decimal oferta, UsuarioModelDBO usuario)
        {
            throw new NotImplementedException();
        }
    }
}

