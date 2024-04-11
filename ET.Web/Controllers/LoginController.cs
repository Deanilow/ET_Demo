using ET.Domain.Entities;
using ET.Domain.Interface.IService;
using ET.Web.Core;
using ET.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ET.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAutenticacionService _IIAutenticacionService;

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IAutenticacionService IIAutenticacionService)
        {
            _IIAutenticacionService = IIAutenticacionService;
            _logger = logger;
        }
        public IActionResult Index(string returnUrl)
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _IIAutenticacionService.FindUsuario(model.Email, ToBase64Encode(model.Password));

                if (response.Succeeded)
                {
                    await GuardarClaimsSesion(response.Data.Usuario);

                    return await Task.Run(() => RedirectToAction("Index", "Home"));
                }
                else
                {
                    model.Mensaje = response.Message;
                }
            }

            return await Task.Run(() => View("Index", model));
        }

        private async Task GuardarClaimsSesion(Usuario usuario)
        {
            var claims = new[] {
                                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                                new Claim(ClaimTypes.Name, $"{usuario.Nombres} {usuario.Apellidos}"),
                                new Claim(ClaimTypes.Role, usuario.Perfil)
                            };

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            await HttpContext.Session.SetData("User", usuario);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await ExpireSession();
            return await Task.Run(() => RedirectToAction("Index"));
        }

        private async Task ExpireSession()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            CookieOptions option = new CookieOptions();
            if (Request.Cookies[".AspNetCore.Session"] != null)
            {
                option.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Append(".AspNetCore.Session", "", option);
            }
        }
        private string ToBase64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
