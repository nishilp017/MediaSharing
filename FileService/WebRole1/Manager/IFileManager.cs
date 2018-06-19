using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebRole1.Manager
{
    public interface IFileManager
    {
        Task<string> UploadFile(HttpPostedFile file);
    }
}