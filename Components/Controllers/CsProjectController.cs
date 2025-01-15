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
}

