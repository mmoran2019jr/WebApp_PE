using DataAccess.BsnLogic.Interfaces;
using DataAccess.BsnLogic.Models;
using DataAccess.BsnLogic.Repositories;
using DataAccess.BsnLogic.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Services
{
    public class FormulaService : IFormulaService
    {
        private readonly IFormulaRepository _repository;

        public FormulaService(IFormulaRepository repository)
        {
            _repository = repository;
        }

        public List<Producto> ObtenerProductos()
        {
            return _repository.ObtenerProductos();
        }

        public List<Formula> ObtenerFormulas()
        {
            return _repository.ObtenerFormulas();
        }

        public async Task CrearFormulaAsync(FormulaViewModel model)
        {
            var formula = new Formula
            {
                IdProducto = model.IdProducto,
                Nombre = model.Nombre,
                Cantidad = model.Cantidad,
                Materiales = new List<FormulaMateriales>()
            };

            foreach (var mat in model.Materiales)
            {
                formula.Materiales.Add(new FormulaMateriales
                {
                    IdProducto = mat.IdProducto,
                    Nombre = mat.Nombre,
                    Cantidad = mat.Cantidad
                    // No pongas IdFormula: EF Core lo infiere por la relación
                });
            }

            await _repository.GuardarFormulaConMaterialesAsync(formula);
        }
    }
}
