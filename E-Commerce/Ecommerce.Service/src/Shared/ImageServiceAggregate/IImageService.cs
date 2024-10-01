using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.src.Shared.ImageServiceAggregate
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile image);
        bool DeleteImage(string imagePath);
    }
}
