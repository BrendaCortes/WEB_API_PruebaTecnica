using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.Context;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndPoindsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EndPoindsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/logins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
            return await _context.Set<Login>().ToListAsync();
        }

        // POST: api/logins
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login login)
        {
            _context.Set<Login>().Add(login);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLogins), new { id = login.User_id }, login);
        }

        // PUT: api/logins/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin(int id, Login login)
        {
            if (id != login.User_id)
                return BadRequest();

            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Set<Login>().Any(e => e.User_id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/logins/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            var login = await _context.Set<Login>().FindAsync(id);
            if (login == null)
                return NotFound();

            _context.Set<Login>().Remove(login);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
