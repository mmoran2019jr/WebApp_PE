using DataAccess.BsnLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Repositories
{
    public interface IFormulaRepository
    {
        List<Producto> ObtenerProductos();
        List<Formula> ObtenerFormulas();
        Task GuardarFormulaConMaterialesAsync(Formula formula);
    }
}
