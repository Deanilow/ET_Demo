using System.ComponentModel.DataAnnotations;
namespace ET.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El Correo es Requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Clave Secreta es Requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
