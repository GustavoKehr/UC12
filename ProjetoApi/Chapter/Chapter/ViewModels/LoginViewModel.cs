using System.ComponentModel.DataAnnotations;

namespace Chapter.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string? Senha { get; set; }
    }
}