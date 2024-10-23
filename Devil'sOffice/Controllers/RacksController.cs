using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Devil_sOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacksController : ControllerBase
    {
        readonly _666Context _context;
        public RacksController(_666Context _context)
        {
            this._context = _context;
        }

        [HttpPost("AddRack")]
        public async Task<ActionResult> AddRack(Rack rack)
        {
            try
            {
                rack.IdDevilNavigation = null;
                _context.Racks.Add(rack);
                await _context.SaveChangesAsync();
                return Ok("Стеллаж создан!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateRack")]
        public async Task<ActionResult> UpdateRack(Rack rack)
        {
            _context.Racks.Update(rack);
            await _context.SaveChangesAsync();
            return Ok("Стеллаж обновился!");
        }

        [HttpPost("DeleteRack")]
        public async Task<ActionResult> DeleteRack(RackBl rack)
        {
            var o = _context.Racks.Find(rack.Id);
            _context.Racks.Remove(o);
            await _context.SaveChangesAsync();
            return Ok("Стеллаж умер!");
        }

        [HttpPost("GetRacks")]
        public async Task<List<Rack>> GetRacks()
        {
            List<Rack> racks = _context.Racks.Include(s => s.IdDevilNavigation).OrderBy(c => c.Id).ToList();
            return racks;
        }
    }
}
