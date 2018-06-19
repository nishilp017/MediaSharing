using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WebRole1.BlobAccess
{
    public interface IBlobAccess
    {
        Task<bool> UploadFileAsync(HttpPostedFile file);

        Task<HttpResponseMessage> DownloadFileAsync(string fileName);
    }
}