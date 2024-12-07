using Website.Components.Models;
using System;
using System.IO;

namespace Website.Components.Data
{
    public class DbInitializer
    {
        public static void Initialize(ImageContext context)
        {
            List<Image> list = new List<Image>();
            if (context.Images.Any())
            {
                return;   // DB has been seeded
            }
            string rootFolderPath = @"C:\Users\bachi\Documents\code projects\Website\Website\Pictures\";

            // Get all subfolders
            string[] subfolders = Directory.GetDirectories(rootFolderPath);

            foreach (string subfolder in subfolders)
            {
                // subfolder name
                string subfolderName = Path.GetFileName(subfolder);

                // Get all image files in the subfolder
                string[] imageFiles = Directory.GetFiles(subfolder, "*.*").ToArray();


                foreach (string imageFile in imageFiles)
                {
                    list.Add(new Image { Event = subfolderName, Path = Path.GetFileName(imageFile) });
                }
            }
            context.Images.AddRange(list);
            context.SaveChanges();

        }
    }
}
