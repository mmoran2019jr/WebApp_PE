using System.ComponentModel.DataAnnotations;

namespace DataAccess.BsnLogic.ViewModels
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
