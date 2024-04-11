using AutoMapper;
using ET.Domain.Entities;
using ET.Domain.Interface.IService;
using ET.Web.Base;
using ET.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ET.Web.Controllers
{
    public class SedeOlimpicaController : BaseController
    {
        private readonly ISedeOlimpicaService _ISedeOlimpicaService;
        private readonly IMapper _mapper;

        public SedeOlimpicaController(ISedeOlimpicaService ISedeOlimpicaService, IMapper mapper)
        {
            _ISedeOlimpicaService = ISedeOlimpicaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _ISedeOlimpicaService.GetAll();

            return View(_mapper.Map<IEnumerable<SedeOlimpicaModel>>(response.Data));
        }

        public async Task<IActionResult> Create()
        {
            var model = new SedeOlimpicaModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SedeOlimpicaModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _ISedeOlimpicaService.Insert(_mapper.Map<SedeOlimpica>(model, opt => opt.AfterMap((src, dest) => dest.UsuarioCreador = UsuarioActual.Id)));

                if (response.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    model.Mensaje = response.Message;

                    return await Task.Run(() => View("Create", model));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Update(Guid IdSede)
        {
            var response = await _ISedeOlimpicaService.Find(IdSede);

            if (response.Succeeded) return View(_mapper.Map<SedeOlimpicaModel>(response.Data));

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SedeOlimpicaModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _ISedeOlimpicaService.Update(_mapper.Map<SedeOlimpica>(model, opt => opt.AfterMap((src, dest) => dest.UsuarioModificador = UsuarioActual.Id)));

                if (response.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    model.Mensaje = response.Message;
                    return await Task.Run(() => View("Update", model));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(Guid IdSede)
        {
            var response = await _ISedeOlimpicaService.Find(IdSede);

            if (response.Succeeded) return View(_mapper.Map<SedeOlimpicaModel>(response.Data));

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SedeOlimpicaModel model)
        {
            var response = await _ISedeOlimpicaService.Delete(Guid.Parse(model.Id.ToString()));

            if (response.Succeeded) return RedirectToAction(nameof(Index));
          
            return View(model);
        }
    }
}
