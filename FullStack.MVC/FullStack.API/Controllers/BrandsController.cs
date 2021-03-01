using FullStack.API.Model;
using FullStack.API.Repositories;
using FullStack.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FullStack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _repository;

        public BrandsController(IBrandRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Brand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetAll()
        {
            var brands = _repository.GetAll();
            return brands.Count > 0 ? Ok(brands) : NotFound();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Brand), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _repository.GetAsync(id);
            return brand != null ? Ok(brand) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Brand), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(Brand brand)
        {
            var result = await _repository.AddAsync(brand);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Brand), (int)HttpStatusCode.OK)]
        public IActionResult Update(Brand brand)
        {
            _repository.Update(brand);
            return Ok();
        }
    }
}
