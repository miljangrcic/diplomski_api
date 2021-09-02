using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Diplomski.Helpers
{
    public static class ImageUploader
    {
        public static string SaveImage(IFormFile image, string folderPath)
        {
            string safeFileName = GenerateFileName(image);
            string pathToSave = Path.Combine(folderPath, safeFileName);

            using(var stream = File.Create(pathToSave))
            {
                image.CopyTo(stream);
            }

            return safeFileName;
        }

        public static void DeleteImage(string pathToImage)
        {
            File.Delete(pathToImage);
        }


        private static string GenerateFileName(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            return $"{Guid.NewGuid()}{extension}";
        }
    }
}
