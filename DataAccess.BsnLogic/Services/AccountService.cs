using DataAccess.BsnLogic.Interfaces;
using DataAccess.BsnLogic.Models;
using DataAccess.BsnLogic.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AccountService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario? ValidarUsuario(string nombre, string contraseña)
        {
            return _usuarioRepository.ValidarUsuario(nombre, contraseña);
        }

        public ClaimsPrincipal GenerarClaimsPrincipal(Usuario usuario)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nombre ?? ""),
            new Claim(ClaimTypes.Role, usuario.Grupo?.Nombre ?? "")
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}
