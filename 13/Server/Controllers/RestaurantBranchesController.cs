using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantBranchesController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public RestaurantBranchesController(RestaurantContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<RestaurantBranch>> Get()
        {
            var branches = from r in _context.RestaurantBranches
                           orderby r.City
                           select r;

            return branches.ToList();
        }
    }
}
