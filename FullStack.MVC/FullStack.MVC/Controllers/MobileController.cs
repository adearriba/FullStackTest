using FullStack.Models;
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
        public ActionResult Create()
        {
            return View();
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
        public async Task<ActionResult> Edit(int id)
        {
            var mobile = await _mobileService.GetByIdAsync(id);
            return View(mobile);
        }

        // POST: MobileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm] Mobile mobileInput)
        {
            try
            {
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
