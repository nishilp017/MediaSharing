using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebRole1.DataAccess;
using WebRole1.Manager;
using WebRole1.Models;

namespace WebRole1.Controllers
{
    public class FilesController : ApiController
    {
        public async Task<string> Post()
        {
            var request = HttpContext.Current.Request;
            var file = request.Files[0];

            var fileManager = new FileManager();
            try
            {
                var result = await fileManager.UploadFile(file);
                return result;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetFile([FromUri] string id)
        {
            var fileManager = new FileManager();
            return await fileManager.DownloadFile(id);
        }

        public async Task<IEnumerable<FileResultModel>> GetAllFile()
        {
            var fileManager = new FileManager();
            return (await fileManager.GetAllFiles())?.Select(c => new FileResultModel
            {
                FileName = c.RowKey,
                FileId = c.PartitionKey,
                ContentLength = c.ContentLength
            }).ToList();
        }
    }
}