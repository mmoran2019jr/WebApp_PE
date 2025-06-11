using System.ComponentModel.DataAnnotations;

namespace WebApp_PE.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
    }
}
