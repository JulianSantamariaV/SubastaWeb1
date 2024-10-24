namespace SubastaWeb.Models.Subasta
{
    public class SubastaAscendente : IStrategySubasta
    {
        private decimal ofertaAnterior;
        private DateTime ultimaOferta;

        public bool Activa => throw new NotImplementedException();

        public void RealizarOferta(decimal cantidad)
        {
            if (cantidad > ofertaAnterior)
            {
                ofertaAnterior = cantidad;
                ultimaOferta = DateTime.Now;
                // Lógica para actualizar la subasta y manejar ofertas
            }
            else
            {
                throw new Exception("La oferta debe ser mayor que la anterior.");
            }
        }

        public void CerrarSubasta()
        {
            // Lógica para cerrar la subasta si no hay nuevas ofertas en un tiempo determinado
        }

        public void IniciarSubasta()
        {
            throw new NotImplementedException();
        }
    }
}
