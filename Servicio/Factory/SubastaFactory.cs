using SubastaWeb.Models.Subasta;

namespace SubastaWeb.Servicio.Factory
{
    public class SubastaFactory
    {
        public static IStrategySubasta CrearSubasta(string tipoDeSubasta)
        {
            switch (tipoDeSubasta.ToLower())
            {
                case "ascendente":
                    return new SubastaAscendente();
                case "descendente":
                    return new SubastaDescendente();
                case "cerrada":
                    return new SubastaCerrada();
                default:
                    throw new ArgumentException("Tipo de subasta no soportado");
            }
        }
    }
}