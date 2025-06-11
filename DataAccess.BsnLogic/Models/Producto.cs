using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Unidad { get; set; }
    }
}
