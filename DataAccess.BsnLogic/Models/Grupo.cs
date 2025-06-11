using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Models
{
    public class Grupo
    {
        public int IdGrupo { get; set; }
        public string? Nombre { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
