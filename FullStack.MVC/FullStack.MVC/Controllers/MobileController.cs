using FullStack.Models;
using FullStack.MVC.Models.Mobile;
using FullStack.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var mobiles = await _mobileService.GetAllAsync();
            return View(mobiles);
        }

        // GET: MobileController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var mobile = await _mobileService.GetByIdAsync(id);
            return View(mobile);
        }

        // GET: MobileController/Create
        public async Task<ActionResult> Create([FromServices] IBrandService brandService)
        {
            CreateMobileViewModel vm = new CreateMobileViewModel();
            vm.Brands = await brandService.GetAllAsync();
            return View(vm);
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
                return View();
            }
        }

        // GET: MobileController/Edit/5
        public async Task<ActionResult> Edit([FromServices] IBrandService brandService, int id)
        {
            var mobile = await _mobileService.GetByIdAsync(id);

            CreateMobileViewModel vm = new CreateMobileViewModel();
            vm.Brands = await brandService.GetAllAsync();
            vm.Model = mobile.Model;

            return View(vm);
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
            var mobile = await _mobileService.GetByIdAsync(id);
            return View(mobile);
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
