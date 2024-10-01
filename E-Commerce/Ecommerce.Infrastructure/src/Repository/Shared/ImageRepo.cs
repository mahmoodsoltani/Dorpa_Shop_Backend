using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.Shared;

namespace Ecommerce.Infrastructure.src.Repository.Shared
{
    public class ImageRepo(IWebHostEnvironment environment) : IImageRepo
    {
        private readonly IWebHostEnvironment _environment = environment;

        public bool DeleteImage(string imagePath)
        {
            try
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            string rootPath = Path.Combine("images", "product");

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            var extension = Path.GetExtension(image.FileName);
            var imageName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(rootPath, imageName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
