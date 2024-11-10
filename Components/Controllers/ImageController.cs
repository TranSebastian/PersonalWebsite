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

    [HttpPost]
    public async Task<IActionResult> Create(Image newImage)
    {
        await _imageContext.Images.AddAsync(newImage);
        await _imageContext.SaveChangesAsync();

        var pizza = newImage;
        return CreatedAtAction(nameof(GetById), new { id = pizza!.Id }, pizza);
    }

    [HttpPut("{id}/updateevent")]
    public async Task<IActionResult> UpdateEvent(int id, string desc)
    {
        var imageToUpdate = await _imageContext.Images.FindAsync(id);

        if (imageToUpdate is not null)
        {
            imageToUpdate.Event = desc;
            await _imageContext.SaveChangesAsync();
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var image = await _imageContext.Images.FindAsync(id);

        if (image is not null)
        {
            _imageContext.Images.Remove(image);
            _imageContext.SaveChanges();
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}