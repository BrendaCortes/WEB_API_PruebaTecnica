using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
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
            return await _context.Login.ToListAsync();
        }

        // POST: api/logins
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login login)
        {
            // Verificar si el usuario existe
            var userExists = await _context.Users.AnyAsync(u => u.User_id == login.User_id);
            if (!userExists)
            {
                return BadRequest($"El usuario con ID {login.User_id} no existe.");
            }

            _context.Set<Login>().Add(login);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLogins), new { id = login.id }, login);
        }

        // PUT: api/logins/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin(int id, Login login)
        {
            if (id != login.id)
                return BadRequest("El id en la URL no coincide con el id en el cuerpo.");

            var existingLogin = await _context.Login.FindAsync(id);
            if (existingLogin == null)
                return NotFound($"No se encontró el login con ID {id}.");

            
            existingLogin.TipoMov = login.TipoMov;
            existingLogin.fecha = login.fecha;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Login.Any(e => e.id == id))
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
            var login = await _context.Login.FindAsync(id);
            if (login == null)
                return NotFound($"No se encontró el login con ID {id}.");

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportCsv()
        {
            var users = await _context.Users
                .Include(u => u.Area)
                .Include(u => u.Logins)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Login,NombreCompleto,Area,TotalHoras");

            foreach (var user in users)
            {
                var logins = user.Logins
                    .OrderBy(l => l.fecha)
                    .ToList();

                double totalHours = 0;

                // Calcular tiempo trabajado
                for (int i = 0; i < logins.Count - 1; i++)
                {
                    if (logins[i].TipoMov == 1 && logins[i + 1].TipoMov == 0)
                    {
                        var diff = logins[i + 1].fecha - logins[i].fecha;
                        totalHours += diff.TotalHours;
                    }
                }

                string nombreCompleto = $"{user.Nombres} {user.ApellidoPaterno} {user.ApellidoMaterno}";
                string area = user.Area?.AreaName ?? "N/A";

                sb.AppendLine($"{user.Login},{nombreCompleto},{area},{totalHours:F2}");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(sb.ToString());

            return File(buffer, "text/csv", "ReporteHoras.csv");
        }

    }
}
