using System.ComponentModel.DataAnnotations;

namespace DeepSeekUI.Application.Dtos.Identity
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage ="El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage ="Formato ivalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener como minimo 6 caracteres")]
        public string Password { get; set; }
    }
}