using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Devil_sOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevilsController : ControllerBase
    {
        readonly _666Context _context;
        public DevilsController(_666Context _context)
        {
            this._context = _context;
        }

        [HttpPost("AddDevil")]
        public async Task<ActionResult> AddDevil(Devil devil)
        {
            _context.Devils.Add(devil);
            await _context.SaveChangesAsync();
            return Ok("Дьявол создан!");
        }

        [HttpPost("UpdateDevil")]
        public async Task<ActionResult> UpdateDevil(Devil devil)
        {
            _context.Devils.Update(devil);
            await _context.SaveChangesAsync();
            return Ok("Дьявол обновился!");
        }

        [HttpPost("DeleteDevil")]
        public async Task<ActionResult> DeleteDevil(Devil devil)
        {
            Disposal disposal = (Disposal)_context.Disposals.Where(c => c.Title == devil.Nick && c.Year == devil.Year);
            _context.Disposals.Add(disposal);
            _context.Devils.Remove(devil);
            await _context.SaveChangesAsync();
            return Ok("Дьявол стёрся!");
        }
    }
}
