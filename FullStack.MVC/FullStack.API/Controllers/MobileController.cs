using FullStack.API.Model;
using FullStack.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FullStack.API.Controllers
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
        [ProducesResponseType(typeof(Brand), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var mobile = await _repository.GetAsync(id);
            return mobile != null ? Ok(mobile) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Brand), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(Mobile mobile)
        {
            var result = await _repository.AddAsync(mobile);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Brand), (int)HttpStatusCode.OK)]
        public IActionResult Update(Mobile mobile)
        {
            _repository.Update(mobile);
            return Ok();
        }
    }
}
