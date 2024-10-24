namespace SubastaWeb.Models.Subasta
{
    public interface IStrategySubasta
    {
        void IniciarSubasta();
        void CerrarSubasta();
        bool Activa { get; }
    }
}
