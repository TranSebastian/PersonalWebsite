using Website.Components.Data;
using Website.Components.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Website.Components.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    ImageContext _imageContext;

    public ImageController(ImageContext imageContext)
    {
        _imageContext = imageContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Image>>> GetAll()
    {
        return await _imageContext.Images.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Image>> GetById(int id)
    {
        var image = await _imageContext.Images.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id); 

        if (image is not null)
        {
            return image;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("getevent/{Event}")]
    public async Task<ActionResult<List<int>>> GetEvent(string Event)
    {
        return await _imageContext.Images.AsNoTracking().Where(x => x.Event == Event).Select(item => item.Id).ToListAsync();
    }

    [HttpGet("getImage/{Id}")]
    public async Task<IActionResult> GetImage(int Id)
    {
        var image = await _imageContext.Images.FindAsync(Id);
        if (image is not null && image.Path is not null && image.Event is not null)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Pictures", image.Event, image.Path);

            var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            
            return File(stream, "image/jpeg");
        }

        return NotFound();
    }
}