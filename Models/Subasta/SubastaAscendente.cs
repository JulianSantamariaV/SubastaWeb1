using SubastaWeb.Models.Producto;
using SubastaWeb.Models.Usuario;
using System;
using System.Timers;
using System.Web.Mvc;

namespace SubastaWeb.Models.Subasta
{
    public class SubastaAscendente : IStrategySubasta
    {
        private decimal ofertaAnterior;
        private DateTime inicioSubasta;
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
            // Tomar el tiempo
            inicioSubasta = DateTime.Now;
            // Configurar el temporizador para 5 minutos
            timer = new System.Timers.Timer(300000);
            timer.Elapsed += OnTiempoTerminado;
            timer.AutoReset = false; // Solo se ejecuta una vez
            Activa = true;
            timer.Start();
            Console.WriteLine("Subasta iniciada. Se cerrará en 5 minutos.");
        }

        public TimeSpan ObtenerTiempoTranscurrido()
        {
            if (Activa)
            {
                return DateTime.Now - inicioSubasta;
            }
            else
            {
                return TimeSpan.Zero;
            }
        }

        public void RealizarOferta(ProductoModelDBO producto, decimal oferta, UsuarioModelDBO usuario)
        {
            if (oferta > producto.PrecioFinal)
            {
                producto.PrecioFinal = oferta;                
            }
            else
            {
                throw new Exception("La oferta debe ser mayor al precio actual.");
            }
        }

        private void OnTiempoTerminado(object source, ElapsedEventArgs e)
        {
            CerrarSubasta();
        }
    }
}
