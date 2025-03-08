using Microsoft.AspNetCore.Http;

namespace AlpaStock.Core.DTOs.Response.Blog
{
    public class UploadPictureReq
    {
        public string fileName { get; set; }
        public IFormFile file { get; set; }
    }
}
