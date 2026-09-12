using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Encontros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncontrosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EncontrosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Encontros.ToListAsync();
            return Ok(model);
        }>
    }
}
