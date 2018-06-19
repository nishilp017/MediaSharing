using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebRole1.DataAccess;

namespace WebRole1.Manager
{
    public class FileManager : IFileManager
    {
        public async Task<string> UploadFile(HttpPostedFile file)
        {
            var dataAccess = new FileDataAccess();
            var fileName = file.FileName;
            var fileId = Guid.NewGuid().ToString();
            try
            {
                var writeResult = await dataAccess.WriteFileAsync(new FileEntity(fileName, fileId));
                return writeResult;
            }
            catch(StorageException exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}