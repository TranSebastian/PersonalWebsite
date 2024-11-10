using Website.Components.Data;
using Website.Components.Models;
using Microsoft.EntityFrameworkCore;

namespace Website.Components.Services
{
    public class ImageService
    {
        private readonly ImageContext _context;

        public ImageService(ImageContext context)
        {
            _context = context;
        }

        public IEnumerable<Image> GetAll()
        {
            return _context.Images
                .AsNoTracking()
                .ToList();
        }

        public Image? GetById(int id)
        {
            //.Include(p => p.Path)
            //.Include(p => p.Event)
            return _context.Images
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
        }

        public Image Create(Image newImage)
        {
            _context.Images.Add(newImage);
            _context.SaveChanges();

            return newImage;
        }

        public void UpdateEvent(int imageId, string description)
        {
            var imageToUpdate = _context.Images.Find(imageId);
            if (imageToUpdate is not null)
            {
                imageToUpdate.Event = description;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var ImageToDelete = _context.Images.Find(id);
            if (ImageToDelete is not null)
            {
                _context.Images.Remove(ImageToDelete);
                _context.SaveChanges();
            }
        }
    }
}
