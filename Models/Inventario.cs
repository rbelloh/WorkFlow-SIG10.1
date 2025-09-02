using System.ComponentModel.DataAnnotations;

namespace WorkFlow_SIG10._1.Models
{
    public class Inventario
    {
        [Key]
        public int MaterialID { get; set; }
        public string NombreMaterial { get; set; } = string.Empty;
        public int CantidadStock { get; set; }
        public string UnidadMedida { get; set; } = string.Empty;
        public string UbicacionBodega { get; set; } = string.Empty;
        public int StockMinimo { get; set; }
    }
}