using Website.Components.Models;

namespace Website.Components.Data
{
    public class DbInitializer
    {
        public static void Initialize(ImageContext context)
        {

            if (context.Images.Any())
            {
                return;   // DB has been seeded
            }

            var images = new Image[]
            {
                new Image
                {
                    Path = "image/obama",
                    Event = "ASA"

                },
                new Image
                {
                    Path = "image/birthday",
                    Event = "Birthday"
                }
            };

            context.Images.AddRange(images);
            context.SaveChanges();

        }
    }
}
