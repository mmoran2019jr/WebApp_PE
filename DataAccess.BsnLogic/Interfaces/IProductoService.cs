using DataAccess.BsnLogic.Models;
using DataAccess.BsnLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoListadoViewModel>> ObtenerTodosAsync();
        Task<ProductoListadoViewModel?> ObtenerPorIdAsync(int id);
        Task CrearAsync(ProductoViewModel model);
        Task ActualizarAsync(int id, ProductoViewModel model); // ← aquí el cambio
        Task EliminarAsync(int id);
    }

}
