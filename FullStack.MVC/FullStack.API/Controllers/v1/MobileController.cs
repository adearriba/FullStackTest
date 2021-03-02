using FullStack.API.Repositories.Interfaces;
using FullStack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FullStack.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MobileController : Controller
    {
        private readonly IMobileRepository _repository;

        public MobileController(IMobileRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Mobile>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetAll()
        {
            var mobiles = _repository.GetAll();
            return mobiles.Count > 0 ? Ok(mobiles) : NotFound();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Mobile), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var mobile = await _repository.GetAsync(id);
            return mobile != null ? Ok(mobile) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Mobile), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(Mobile mobile)
        {
            var result = await _repository.AddAsync(mobile);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Mobile), (int)HttpStatusCode.OK)]
        public IActionResult Update(Mobile mobile)
        {
            _repository.Update(mobile);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Remove(int id)
        {
            var mobile = await _repository.GetAsync(id);
            _repository.Remove(mobile);
            return Ok();
        }
    }
}
