using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Devil_sOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisposalsController : ControllerBase
    {
        readonly _666Context _context;
        public DisposalsController(_666Context _context)
        {
            this._context = _context;
        }
        [HttpPost("DisposalDevil")]
        public async Task<ActionResult> DisposalDevil(Devil devil)
        {
            Disposal disposal = new Disposal { Title = devil.Nick, Year = devil.Year };
            _context.Disposals.Add(disposal);
            await _context.SaveChangesAsync();
            return Ok("Дьявол добавлен в утилизацию!");
        }

        [HttpPost("DisposalRack")]
        public async Task<ActionResult> DisposalRack(RackBl rack)
        {
            Disposal disposal = new Disposal { Title = rack.Title, Year = rack.YearBuy };
            _context.Disposals.Add(disposal);
            await _context.SaveChangesAsync();
            return Ok("Стеллаж добавлен в утилизацию!");
        }
    }
}
