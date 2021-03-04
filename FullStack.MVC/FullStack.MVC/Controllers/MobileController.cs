using FullStack.Models;
using FullStack.MVC.Models.Mobile;
using FullStack.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStack.MVC.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public class MobileController : Controller
    {
        private readonly IMobileService _mobileService;

        public MobileController(IMobileService mobileService)
        {
            _mobileService = mobileService;
        }

        // GET: MobileController
        public async Task<ActionResult> Index()
        {
            try
            {
                var mobiles = await _mobileService.GetAllAsync();
                return View(mobiles);
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                List<Mobile> mobiles = new List<Mobile>();
                return View(mobiles);
            }
        }

        // GET: MobileController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var mobile = await _mobileService.GetByIdAsync(id);
                return View(mobile);
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: MobileController/Create
        public async Task<ActionResult> Create([FromServices] IBrandService brandService)
        {
            try
            {
                CreateMobileViewModel vm = new CreateMobileViewModel();
                vm.Brands = await brandService.GetAllAsync();

                if(vm.Brands.Count == 0)
                {
                    ViewData["error"] = "No existe ninguna marca creada todavía. Por favor crea una marca primero.";
                }

                return View(vm);
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: MobileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] Mobile mobileInput)
        {
            try
            {
                var mobile = await _mobileService.AddAsync(mobileInput);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                return View();
            }
        }

        // GET: MobileController/Edit/5
        public async Task<ActionResult> Edit([FromServices] IBrandService brandService, int id)
        {
            try
            {
                var mobile = await _mobileService.GetByIdAsync(id);

                CreateMobileViewModel vm = new CreateMobileViewModel
                {
                    Model = mobile.Model,
                    BrandId = mobile.BrandId,
                    Description = mobile.Description,
                    BateryDescription = mobile.BateryDescription,
                    CamaraDescripcion = mobile.CamaraDescripcion,
                    ScreenDescription = mobile.ScreenDescription,
                    StorageDescription = mobile.StorageDescription
                };

                vm.Brands = await brandService.GetAllAsync();

                return View(vm);
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: MobileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm] Mobile mobileInput)
        {
            try
            {
                mobileInput.Id = id;
                await _mobileService.UpdateAsync(mobileInput);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var mobile = await _mobileService.GetByIdAsync(id);
                return View(mobile);
            }
        }

        // GET: MobileController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var mobile = await _mobileService.GetByIdAsync(id);
                return View(mobile);
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: MobileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _mobileService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var mobile = await _mobileService.GetByIdAsync(id);
                return View(mobile);
            }
        }
    }
}
