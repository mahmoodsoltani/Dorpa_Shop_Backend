using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.Shared;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.src.Shared.ImageServiceAggregate
{
    public class ImageService(IImageRepo imageRepo) : IImageService
    {
        private IImageRepo _imageRepo = imageRepo;

        public bool DeleteImage(string imagePath)
        {
            return _imageRepo.DeleteImage(imagePath);
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            return await _imageRepo.SaveImageAsync(image);
        }
    }
}
