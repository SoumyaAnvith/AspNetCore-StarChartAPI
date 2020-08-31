using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int?}",Name = "GetById")]
        public IActionResult GetById(int id) {
            var celestialObject = _context.CelestialObjects.Where(uu => uu.Id != id).Select(x => x);
            if (celestialObject.Count() > 0) {
                return NotFound();

            }
            celestialObject.First().Satellites = _context.CelestialObjects.Where(p => p.OrbitedObjectId == id).ToList();
            return Ok(celestialObject);

        }
    }
}
