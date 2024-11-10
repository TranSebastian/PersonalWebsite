using Website.Components.Services;
using Website.Components.Models;
using Microsoft.AspNetCore.Mvc;

namespace Website.Components.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    ImageService _service;

    public ImageController(ImageService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Image> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Image> GetById(int id)
    {
        var image = _service.GetById(id);

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
    public IActionResult Create(Image newImage)
    {
        var pizza = _service.Create(newImage);
        return CreatedAtAction(nameof(GetById), new { id = pizza!.Id }, pizza);
    }

    [HttpPut("{id}/updateevent")]
    public IActionResult UpdateEvent(int id, string desc)
    {
        var imageToUpdate = _service.GetById(id);

        if (imageToUpdate is not null)
        {
            _service.UpdateEvent(id, desc);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        var image = _service.GetById(id);

        if (image is not null)
        {
            _service.DeleteById(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}