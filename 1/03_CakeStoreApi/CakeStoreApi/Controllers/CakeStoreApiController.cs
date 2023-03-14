using CakeStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CakeStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CakeStoreApiController : ControllerBase
    {
        private readonly IData _data;

        public CakeStoreApiController(IData data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<List<CakeStore>?> GetAll()
        {
            var result = _data.CakesInitializeData();
            return Ok(result);
        }

        [HttpGet("/{id}")]
        public ActionResult<CakeStore?> GetById(int? id)
        {
            var item = _data.GetCakeById(id);
            if (item is null) return NotFound();
            return Ok(item);
        }
    }
}
