using DataAccess.BsnLogic.Models;
using DataAccess.BsnLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Interfaces
{
    public interface IFormulaService
    {
        List<Producto> ObtenerProductos();
        List<Formula> ObtenerFormulas();
        Task CrearFormulaAsync(FormulaViewModel model);
    }
}
