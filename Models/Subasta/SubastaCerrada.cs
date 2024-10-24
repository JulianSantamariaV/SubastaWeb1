namespace SubastaWeb.Models.Subasta
{
    public class SubastaCerrada : IStrategySubasta
    {
        private List<decimal> ofertas;

        public bool Activa => throw new NotImplementedException();

        public void RealizarOferta(decimal cantidad)
        {
            ofertas.Add(cantidad);
        }

        public void CerrarSubasta()
        {
            // Lógica para adjudicar el producto a la oferta más alta
        }

        public void IniciarSubasta()
        {
            throw new NotImplementedException();
        }
    }

}
