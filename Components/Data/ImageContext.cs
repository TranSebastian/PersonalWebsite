using Microsoft.EntityFrameworkCore;
using Website.Components.Models;

namespace Website.Components.Data
{
    public class ImageContext : DbContext
    {
        public ImageContext (DbContextOptions<ImageContext> options) : base(options) { }

        public DbSet<Image> Images => Set<Image> ();
    }
}
