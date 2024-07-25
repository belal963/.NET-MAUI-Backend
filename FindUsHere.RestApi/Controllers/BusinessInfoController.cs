using FindUsHere.DbConnector;
using FindUsHere.General.Interfaces;
using FindUsHere.General;
using FindUsHere.RestApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Google.Cloud.Storage.V1;


namespace FindUsHere.RestApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BusinessInfoController : BaseController
    {
        #region ctor
        public BusinessInfoController(IDBConnection dBConnection) : base(dBConnection)
        {
            
        }
        #endregion

        #region Business

        [HttpGet("business")]
        public async Task<IActionResult> GetAllBusinessInfosByRadius(double lat, double lon, int radius)
        {
            return await TryCatch(() => {
                var businesslist = _connection.GetAllBusinessInfosByRadius(lat, lon, radius);
                if (businesslist != null)
                {
                    return businesslist;
                }
                else
                {
                    throw new NotFoundException("there are no Businesses");
                }
            });
        }

        [HttpGet("business/${id}")]
        public async Task<IActionResult> GetAllBusinessInfosById(int id)
        {
            return await TryCatch(() => {
                var businesslist = _connection.GetAllBusinessInfosById(id);
                if (businesslist != null)
                {
                    return businesslist;
                }
                else
                {
                    throw new NotFoundException("there are no Businesses");
                }
            });
        }


        [HttpPost("business")]
        public async Task<IActionResult> InsertBus(RestApiBusinessInfo restApiBusinessInfo)
        {
            return await TryCatch(() =>
            {
                var insertBusinessInfos = _connection.InsertBusinessInfos((IBusinessInfo)restApiBusinessInfo);
                if (insertBusinessInfos != null)
                {
                    return JsonSerializer.Serialize<IBusinessInfo>(insertBusinessInfos);
                }
                else
                {
                    throw new NotFoundException("User already Exist");
                }
            });
        }


        [HttpDelete("business/{Id}")]
        public async Task<IActionResult> DeleteBusiness(int Id)
        {
            return await TryCatch(() =>
            {
                var deleteBusiness = _connection.DeleteBusiness(Id);
                if( deleteBusiness == 0) 
                {
                    throw new NotFoundException("there is not user to delete");
                }

            });
        }

        #endregion

        #region Category
        [HttpGet("categories")]
        public async Task<IActionResult> SelectCategory()
        {
            return await TryCatch(() =>
            {
                var category = _connection.SelectAllCategories();
                if (category == null)
                {
                    throw new NotFoundException("there is no Categories found");
                }
                else { return category; }
                
            }); 
        }
        #endregion


        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile fileContent)
        {
            if (fileContent == null || fileContent.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadsFolderPath = @"***********";
            Directory.CreateDirectory(uploadsFolderPath);

            var filePath = Path.Combine(uploadsFolderPath, fileContent.FileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileContent.CopyToAsync(stream);
                }
                return Ok(new { filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("googleGetFile")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            
            var uploadsFolderPath = @"***********";

            // Ensure the uploads folder exists
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var client = StorageClient.Create();
            var stream = new MemoryStream();

            // Download the object from Google Cloud Storage into the memory stream
            var obj = await client.DownloadObjectAsync("****", fileName, stream);
            stream.Position = 0;

            // Create the full local file path
            var localFilePath = Path.Combine(uploadsFolderPath, fileName);

            // Write the stream to the local file
            using (var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }

            // Optionally, return some kind of result indicating success
            return Ok($"File downloaded to {localFilePath}");
        }

        [HttpPost("googlePostFile")]
        public async Task<IActionResult> SendFile([FromBody] FileUpload fileUpload)
        {
            var client = StorageClient.Create();
            var obj = await client.UploadObjectAsync(
                "*****",
                fileUpload.Name,
                fileUpload.Type,
                new MemoryStream(fileUpload.File)
               );


            return Ok();
        }

        public class FileUpload
        {
            public string Name { get; set; }
            public string Type { get; set; }

            public byte[] File { get; set; }


        }

    }
}
