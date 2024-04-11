using System.ComponentModel.DataAnnotations;

namespace ET.Web.Models
{
    public class SedeOlimpicaModel
	{
		public Guid? Id { get; set; } = null;

        [Required(ErrorMessage = "El Nombre es Requerido")]
		public string Nombre { get; set; } 

        [Required(ErrorMessage = "La Presupuesto es Requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "La Presupuesto debe ser mayor o igual a cero")]
        public decimal Presupuesto { get; set; }
		public decimal NumeroComplejos { get; set; }
		public string Mensaje { get; set; } = string.Empty;
    }
}