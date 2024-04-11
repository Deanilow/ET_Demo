using AutoMapper;
using ET.Domain.Entities;
using ET.Domain.Interface.IService;
using ET.Web.Base;
using ET.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ET.Web.Controllers
{
    public class ComplejoDeportivoController : BaseController
	{
		private readonly IComplejoDeportivoService _IComplejoDeportivoService;
		private readonly ISedeOlimpicaService _ISedeOlimpicaService;

		private readonly IMapper _mapper;

		public ComplejoDeportivoController(IComplejoDeportivoService IComplejoDeportivoService, ISedeOlimpicaService ISedeOlimpicaService, IMapper mapper)
		{
			_ISedeOlimpicaService = ISedeOlimpicaService;
			_IComplejoDeportivoService = IComplejoDeportivoService;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var response = await _IComplejoDeportivoService.GetAll();

            return View(_mapper.Map<IEnumerable<ComplejoDeportivoModel>>(response.Data));
		}
		public async Task<IActionResult> Create()
		{
			var model = new ComplejoDeportivoModel();

			var responseSedes = await _ISedeOlimpicaService.GetAll();

			var mapperSedes = _mapper.Map<IEnumerable<SedeOlimpicaModel>>(responseSedes.Data);

			model.SedeList = mapperSedes.Select(x => new SelectListItem
			{
				Text = x.Nombre,
				Value = x.Id.ToString(),
			});

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ComplejoDeportivoModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _IComplejoDeportivoService.Insert(_mapper.Map<ComplejoDeportivo>(model, opt => opt.AfterMap((src, dest) => dest.UsuarioCreador = UsuarioActual.Id)));

				if (response.Succeeded)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					model.Mensaje = response.Message;
				}
			}

            var responseSedes = await _ISedeOlimpicaService.GetAll();

            var mapperSedes = _mapper.Map<IEnumerable<SedeOlimpicaModel>>(responseSedes.Data);

            model.SedeList = mapperSedes.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.Id.ToString(),
            });
            return await Task.Run(() => View("Create", model));
        }

		public async Task<IActionResult> Update(Guid IdComplejo)
		{
			var response = await _IComplejoDeportivoService.Find(IdComplejo);

			var model = new ComplejoDeportivoModel();

			model = _mapper.Map<ComplejoDeportivoModel>(response.Data);

			var responseSedes = await _ISedeOlimpicaService.GetAll();

			var mapperSedes = _mapper.Map<IEnumerable<SedeOlimpicaModel>>(responseSedes.Data);

			model.SedeList = mapperSedes.Select(x => new SelectListItem
			{
				Text = x.Nombre,
				Value = x.Id.ToString(),
			});

			if (response.Succeeded) return View(model);

			return NotFound();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(ComplejoDeportivoModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _IComplejoDeportivoService.Update(_mapper.Map<ComplejoDeportivo>(model, opt => opt.AfterMap((src, dest) => dest.UsuarioModificador = UsuarioActual.Id)));

				if (response.Succeeded)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					model.Mensaje = response.Message;
				}
			}

            var responseSedes = await _ISedeOlimpicaService.GetAll();

            var mapperSedes = _mapper.Map<IEnumerable<SedeOlimpicaModel>>(responseSedes.Data);

            model.SedeList = mapperSedes.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.Id.ToString(),
            });

            return await Task.Run(() => View("Update", model));
        }
		public async Task<IActionResult> Delete(Guid IdComplejo)
		{
			var response = await _IComplejoDeportivoService.Find(IdComplejo);

            var model = new ComplejoDeportivoModel();
            
			model = _mapper.Map<ComplejoDeportivoModel>(response.Data);

            var responseSedes = await _ISedeOlimpicaService.GetAll();

            var mapperSedes = _mapper.Map<IEnumerable<SedeOlimpicaModel>>(responseSedes.Data);

            model.SedeList = mapperSedes.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.Id.ToString(),
            });

            if (response.Succeeded) return View(model);

			return NotFound();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(ComplejoDeportivoModel model)
		{
			var response = await _IComplejoDeportivoService.Delete(Guid.Parse(model.Id.ToString()));

			if (response.Succeeded) return RedirectToAction(nameof(Index));

			return View(model);
		}
	}
}
