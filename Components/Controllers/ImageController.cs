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
            string imagePath = Path.Combine("C:\\Users\\bachi\\Documents\\code projects\\Website\\Website\\Pictures\\", image.Event, image.Path);
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

            return File(fileBytes, "image/jpeg");
        }

        return NotFound();
    }

    [HttpGet("getEventImages/{Event}")]
    public async Task<ActionResult<List<string>>> GetEventImages (string Event)
    {
        List<Image> ids = await _imageContext.Images.AsNoTracking().Where(x => x.Event == Event).ToListAsync();
        List<string> urls = new List<string>();


        if (ids is not null)
        {
            foreach (Image id in ids)
            {
                if (id.Path is not null && id.Event is not null)
                {
                    string imagePath = Path.Combine("C:\\Users\\bachi\\Documents\\code projects\\Website\\Website\\Pictures\\", id.Event, id.Path);
                    
                    byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

                    // Convert the byte array to a base64 string
                    string base64String = Convert.ToBase64String(fileBytes);

                    // Set the image source
                    string imageUrl = $"data:image/jpeg;base64,{base64String}";

                    urls.Add(imageUrl);
                }
            }
            return urls;
        }
        else
        {
            return NotFound();
        }
        
        

    }

}