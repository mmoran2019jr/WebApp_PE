using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? ContraseñaHash { get; set; }

        public int IdGrupo { get; set; }
        public Grupo? Grupo { get; set; }
    }
}
