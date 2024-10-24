namespace SubastaWeb.Models.Producto
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ImgUrl { get; set; }
        public string Categoria { get; set; }
        public string TipoDeSubasta { get; set; }
        public decimal PrecioInicial { get; set; }
        public decimal PrecioFinal { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public bool EnLiquidacion { get; set; }
    }

}
