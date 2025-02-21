using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp4p1.Models.EntityFramework;

[Route("api/[controller]")]
[ApiController]
public class UtilisateursController : ControllerBase
{
    private readonly FilmRatingsDBContext _context;

    public UtilisateursController(FilmRatingsDBContext context)
    {
        _context = context;
    }

    // GET: api/Utilisateurs
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
    {
        return await _context.Utilisateurs.ToListAsync();
    }

    // GET: api/Utilisateurs/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
    {
        var utilisateur = await _context.Utilisateurs.FindAsync(id);
        if (utilisateur == null)
        {
            return NotFound();
        }
        return utilisateur;
    }

    // PUT: api/Utilisateurs/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
    {
        if (id != utilisateur.UtilisateurId)
        {
            return BadRequest();
        }
        _context.Entry(utilisateur).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UtilisateurExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    // POST: api/Utilisateurs
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
    {
        var existingUser = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Mail == utilisateur.Mail);

        if (existingUser != null)
        {
            return Conflict(new { message = "Un utilisateur avec cet email existe déjà" });
        }

        _context.Utilisateurs.Add(utilisateur);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUtilisateurById), new { id = utilisateur.UtilisateurId }, utilisateur);
    }

    // DELETE: api/Utilisateurs/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUtilisateur(int id)
    {
        var utilisateur = await _context.Utilisateurs.FindAsync(id);
        if (utilisateur == null)
        {
            return NotFound();
        }
        _context.Utilisateurs.Remove(utilisateur);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // GET: api/Utilisateurs/GetByMail/email@example.com
    [HttpGet("email/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string email)
    {
        var utilisateur = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Mail == email);

        if (utilisateur == null)
        {
            return NotFound();
        }

        return utilisateur;
    }

    private bool UtilisateurExists(int id)
    {
        return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
    }
}