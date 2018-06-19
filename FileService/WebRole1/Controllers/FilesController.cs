using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebRole1.Manager;

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
    }
}