using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebRole1.DataAccess;

namespace WebRole1.Manager
{
    public interface IFileManager
    {
        Task<string> UploadFile(HttpPostedFile file);

        Task<HttpResponseMessage> DownloadFile(string fileId);

        Task<IEnumerable<FileEntity>> GetAllFiles();
    }
}