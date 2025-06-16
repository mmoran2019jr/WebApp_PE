using DataAccess.BsnLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Repositories
{
    public class FormulaRepository : IFormulaRepository
    {
        private readonly ApplicationDbContext _context;

        public FormulaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Producto> ObtenerProductos()
        {
            return _context.Productos.ToList();
        }

        public List<Formula> ObtenerFormulas()
        {
            return _context.Formulas
                .Include(f => f.Producto)
                .Include(f => f.Materiales)
                    .ThenInclude(m => m.Producto)
                .ToList();
        }

        public async Task GuardarFormulaConMaterialesAsync(Formula formula)
        {
            _context.Formulas.Add(formula);
            await _context.SaveChangesAsync();
        }
    }
}
