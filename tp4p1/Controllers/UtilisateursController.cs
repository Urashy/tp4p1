using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using tp4p1.Models.DataManager;
using tp4p1.Models.EntityFramework;
using tp4p1.Models.Repository;

[Route("api/[controller]")]
[ApiController]
public class UtilisateursController : ControllerBase
{
    private readonly UtilisateurManager utilisateurManager;
    //private readonly FilmRatingsDBContext _context;
    private readonly IDataRepository<Utilisateur> dataRepository;
    public UtilisateursController(IDataRepository<Utilisateur> dataRepo)
    {
        dataRepository = dataRepo;
    }

    // GET: api/Utilisateurs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
    {
        return dataRepository.GetAll();
    }
    // GET: api/Utilisateurs/5
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
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
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
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
        var utilisateur = dataRepository.GetById(id);
        if (utilisateur == null)
        {
            return NotFound();

        }
        dataRepository.DeleteAsync(utilisateur.Value);
        return NoContent();
    }
    //private bool UtilisateurExists(int id)
    //{
    // return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
    //}
}