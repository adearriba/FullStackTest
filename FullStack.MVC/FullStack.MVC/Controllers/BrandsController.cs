using FullStack.Models;
using FullStack.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStack.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BrandsController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // GET: BrandsController
        public async Task<ActionResult> Index()
        {
            try
            {
                var brands = await _brandService.GetAllAsync();
                return View(brands);
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                List<Brand> brands = new List<Brand>();
                return View(brands);
            }
        }

        // GET: BrandsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var brand = await _brandService.GetByIdAsync(id);
                return View(brand);
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: BrandsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] Brand brandInput)
        {
            try
            {
                var brand = await _brandService.AddAsync(brandInput);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BrandsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var brand = await _brandService.GetByIdAsync(id);
                return View(brand);
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm] Brand brandInput)
        {
            try
            {
                await _brandService.UpdateAsync(brandInput);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var brand = await _brandService.GetByIdAsync(id);
                return View(brand);
            }
        }

        // GET: BrandsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var brand = await _brandService.GetByIdAsync(id);
                return View(brand);
            }
            catch
            {
                ViewData["error"] = "Hubo un error. Inténtalo más tarde";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _brandService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var brand = await _brandService.GetByIdAsync(id);
                return View(brand);
            }
        }
    }
}
