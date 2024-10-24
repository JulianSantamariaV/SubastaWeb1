namespace SubastaWeb.Models.Subasta
{
    public class SubastaDescendente : IStrategySubasta
    {
        private decimal precioActual;
        private DateTime tiempoInicio;

        public bool Activa => throw new NotImplementedException();

        public void RealizarOferta(decimal cantidad)
        {
            if (cantidad >= precioActual)
            {
                // Lógica para adjudicar el producto al comprador
            }
        }

        public void CerrarSubasta()
        {
            // Lógica para disminuir el precio a intervalos de tiempo
        }

        public void IniciarSubasta()
        {
            throw new NotImplementedException();
        }
    }
}
