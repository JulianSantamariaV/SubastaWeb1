namespace SubastaWeb.Models.Producto
{
    public class ProductoModelDBO : Producto
    {      
        public int IdProducto { get; set; }
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required string ImgUrl { get; set; }
        public required string Categoria { get; set; }
        public required string TipoDeSubasta { get; set; }
        public decimal PrecioInicial { get; set; }
        public decimal PrecioFinal { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public bool EnLiquidacion { get; set; }

        public override void CalcularPrecioFinal()
        {
            throw new NotImplementedException();
        }
    }
}