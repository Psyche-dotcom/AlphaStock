using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadPhoto(IFormFile file, object id);
    }
}