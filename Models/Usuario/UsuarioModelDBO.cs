using System.ComponentModel.DataAnnotations;

namespace SubastaWeb.Models.Usuario
{
    public class UsuarioModelDBO
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoUsuario { get; set; }
        public double oferta { get; set; }
    }
}
