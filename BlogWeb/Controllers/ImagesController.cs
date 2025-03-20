using System.Net;
using BlogWeb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImages imagesManager;

        public ImagesController(IImages imagesManager)
        {
            this.imagesManager = imagesManager;
        }
        [HttpPost]
        public async Task<IActionResult>UploadAsync(IFormFile file)
        {
           var imageURL= await imagesManager.UploadAsync(file);
            if (imageURL == null) {
                return Problem("Something went wrong!",null,(int) HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageURL });

        }
    }
}
