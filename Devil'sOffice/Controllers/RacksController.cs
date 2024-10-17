using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            _context.Racks.Add(rack);
            await _context.SaveChangesAsync();
            return Ok("Стеллаж создан!");
        }

        [HttpPost("UpdateRack")]
        public async Task<ActionResult> UpdateRack(Rack rack)
        {
            _context.Racks.Update(rack);
            await _context.SaveChangesAsync();
            return Ok("Стеллаж обновился!");
        }

        [HttpPost("DeleteRack")]
        public async Task<ActionResult> DeleteRack(Rack rack)
        {
            Disposal disposal = (Disposal)_context.Disposals.Where(c => c.Title == rack.Title && c.Year == rack.YearBuy);
            _context.Disposals.Add(disposal);
            _context.Racks.Remove(rack);
            await _context.SaveChangesAsync();
            return Ok("Стеллаж умер!");
        }
    }
}
