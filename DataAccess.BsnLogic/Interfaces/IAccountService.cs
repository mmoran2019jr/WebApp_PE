using DataAccess.BsnLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Interfaces
{
    public interface IAccountService
    {
        Usuario? ValidarUsuario(string nombre, string contraseña);
        ClaimsPrincipal GenerarClaimsPrincipal(Usuario usuario);
    }
}
