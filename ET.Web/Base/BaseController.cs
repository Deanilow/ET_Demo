using ET.Domain.Entities;
using ET.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ET.Web.Base
{
    public class BaseController : Controller
    {
        protected Usuario UsuarioActual { get; private set; }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            UsuarioActual = ObtenerUsuarioActual().Result;

            if (UsuarioActual == null)
            {
                HttpContext.Session.Clear();
                context.Result = new RedirectResult("/Login/LogOut");
                return Task.FromResult(0);
            }

            ViewBag.UsuarioActual = UsuarioActual;

            return base.OnActionExecutionAsync(context, next);
        }
        private async Task<Usuario> ObtenerUsuarioActual()
        {
            return await GetSession<Usuario>("User");
        }

        public async Task<T> GetSession<T>(string key)
        {
            return await HttpContext.Session.GetData<T>(key);
        }

        public async Task SetSession(string key, object value)
        {
            await HttpContext.Session.SetData(key, value);
        }
    }
}
