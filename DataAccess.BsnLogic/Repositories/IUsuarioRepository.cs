using DataAccess.BsnLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario? ValidarUsuario(string nombre, string contraseña);
        string CalcularHash(string input);
    }
}
