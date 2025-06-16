using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.ViewModels
{
    public class ProductoViewModel
    {
        public string Nombre { get; set; } = null!;
        public string Unidad { get; set; } = null!;
    }

    public class ProductoListadoViewModel
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Unidad { get; set; } = null!;
    }
}
