using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Models
{
    public class FormulaMateriales
    {
        public int Id { get; set; }

        public int IdFormula { get; set; }
        public Formula? Formula { get; set; }

        public int IdProducto { get; set; }

        public Producto? Producto { get; set; }

        public string? Nombre { get; set; }
        public decimal Cantidad { get; set; }
    }
}
