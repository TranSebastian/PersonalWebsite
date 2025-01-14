using Website.Components.Data;
using Website.Components.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Website.Components.Controllers;

[ApiController]
[Route("[controller]")]
public class CsProjectController : ControllerBase
{
    CsProjectContext _csProjectContext;

    public CsProjectController(CsProjectContext csProjectContext)
    {
        _csProjectContext = csProjectContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<CsProject>>> GetAll()
    {
        return await _csProjectContext.CsProjects.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CsProject>> GetById(int id)
    {
        var image = await _csProjectContext.CsProjects.FindAsync(id);

        if (image is not null)
        {
            return image;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CsProject newCsProject)
    {
        await _csProjectContext.CsProjects.AddAsync(newCsProject);
        await _csProjectContext.SaveChangesAsync();

        var csProject = newCsProject;
        return CreatedAtAction(nameof(GetById), new { id = csProject!.Id }, csProject);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var csProject = await _csProjectContext.CsProjects.FindAsync(id);
        if (csProject is not null)
        {
            _csProjectContext.CsProjects.Remove(csProject);
            await _csProjectContext.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

