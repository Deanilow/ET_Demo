using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ET.Web.Models
{
    public class ComplejoDeportivoModel
    {
		public Guid? Id { get; set; } = null;

        [Required(ErrorMessage = "La Sede es Requerido")]
        public SedeOlimpicaModel SedeOlimpicaModel { get; set; } = new SedeOlimpicaModel();

		[Required(ErrorMessage = "El Nombre es Requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Localizacion es Requerido")]
        public string Localizacion { get; set; }

        [Required(ErrorMessage = "El JefeOrganizacion es Requerido")]
        public string JefeOrganizacion { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> SedeList { get; set; } = Enumerable.Empty<SelectListItem>();
        public string Mensaje { get; set; } = string.Empty;
    }
}
