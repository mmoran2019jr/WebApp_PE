using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataAccess.BsnLogic.Services;
using DataAccess.BsnLogic.Interfaces;
using DataAccess.BsnLogic.ViewModels;

namespace WebApp_PE.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = _accountService.ValidarUsuario(model.Nombre.Trim(), model.Contraseña.Trim());

            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña inválidos");
                return View(model);
            }

            var principal = _accountService.GenerarClaimsPrincipal(usuario);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
