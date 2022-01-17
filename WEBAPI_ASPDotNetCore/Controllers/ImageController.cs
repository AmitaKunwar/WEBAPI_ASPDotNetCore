using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WEBAPI_ASPDotNetCore.Contracts;
using WEBAPI_ASPDotNetCore.Models;
using WEBAPI_ASPDotNetCore.Repository;

namespace WEBAPI_ASPDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageReposWrapper _imgRepos;

        public ImageController(IImageReposWrapper imgRepos)
        {
            _imgRepos = imgRepos;
        }

        [HttpGet]
        public IEnumerable<LoadImage> Get()
        {
            var img = _imgRepos.IMG.FindAll();
            return img;
        }
        [HttpGet("{id}", Name = nameof(GetImageByID))]
        public IEnumerable<LoadImage> GetImageByID(int id)
        {
            var a = _imgRepos.IMG.FindByCondition(x => x.Id.Equals(id));

            if (a == null)
            { return (IEnumerable<LoadImage>)NotFound(); }

            return a;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public ActionResult<LoadImage> ImageUpload([FromForm] LoadImage img)
        {
            try
            {
                var file = Request.Form.Files[0];                

                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    LoadImage result = new LoadImage{Description = img.Description, ImageName= dbPath };
                    _imgRepos.IMG.Create(result);
                    _imgRepos.Save();
                    return CreatedAtAction(nameof(GetImageByID), new { Id= img.Id }, result);

                   // return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet, DisableRequestSizeLimit]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string fileUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), filePath);
           
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        [HttpGet]
        [Route("getPhotos")]
        public IActionResult GetPhotos()
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var img = Directory.EnumerateFiles(pathToRead)
                    .Select(fullPath => Path.Combine(folderName, Path.GetFileName(fullPath)));

                return Ok(new { img });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        
    }
}
