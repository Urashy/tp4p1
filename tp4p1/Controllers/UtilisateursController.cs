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
    private readonly IDataRepository<Utilisateur> utilisateurManager;
    //private readonly FilmRatingsDBContext _context; 

    public UtilisateursController(IDataRepository<Utilisateur> userManager)
    {
        utilisateurManager = userManager;
    }

    // GET: api/Utilisateurs 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
    {
        return utilisateurManager.GetAll();
    }

    // GET: api/Utilisateurs/5 
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
    {
        var utilisateur = utilisateurManager.GetById(id);
        //var utilisateur = await _context.Utilisateurs.FindAsync(id); 

        if (utilisateur == null)
        {
            return NotFound();
        }

        return utilisateur;
    }

    // GET: api/Utilisateurs/toto@titi.fr 
    [HttpGet]
    [Route("[action]/{email}")]
    [ActionName("GetByEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string email)
        { 
            var utilisateur = await utilisateurManager.GetByStringAsync(email); 
            //var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(e => e.Mail.ToUpper() == email.ToUpper()); 
 
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

    var userToUpdate = utilisateurManager.GetById(id);

    if (userToUpdate == null)
    {
        return NotFound();
    }
    else
    {
        utilisateurManager.UpdateAsync(userToUpdate.Value, utilisateur);
        return NoContent();
    }
}

// POST: api/Utilisateurs 
// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
[HttpPost]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    utilisateurManager.AddAsync(utilisateur);

    return CreatedAtAction("GetById", new { id = utilisateur.UtilisateurId }, utilisateur); // GetById : nom de l’action 
}

// DELETE: api/Utilisateurs/5 
[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> DeleteUtilisateur(int id)
{
    var utilisateur = utilisateurManager.GetById(id);
    if (utilisateur == null)
    {
        return NotFound();
    }
    utilisateurManager.DeleteAsync(utilisateur.Value);
    return NoContent();
} 
//private bool UtilisateurExists(int id) 
//{ 
//    
//} 
} 