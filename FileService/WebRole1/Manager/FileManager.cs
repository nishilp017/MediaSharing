using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebRole1.DataAccess;
using WebRole1.BlobAccess;

namespace WebRole1.Manager
{
    public class FileManager : IFileManager
    {
        public async Task<string> UploadFile(HttpPostedFile file)
        {
            var dataAccess = new FileDataAccess();
            var blobAccess = new BlobAccess.BlobAccess();
            var fileName = file.FileName;
            var contetnLength = file.ContentLength;
            var fileId = Guid.NewGuid().ToString();
            try
            {
                var writeResult = await dataAccess.WriteFileAsync(new FileEntity(fileName, fileId, contetnLength.ToString()));
                await blobAccess.UploadFileAsync(file);
                return writeResult;
            }
            catch(StorageException exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<HttpResponseMessage> DownloadFile(string fileId)
        {
            var dataAccess = new FileDataAccess();
            var blobAccess = new BlobAccess.BlobAccess();

            var fileEntity = await dataAccess.GetFileAsync(fileId);

            var file = await blobAccess.DownloadFileAsync(fileEntity.RowKey.ToLowerInvariant());

            return file;
        }

        public async Task<IEnumerable<FileEntity>> GetAllFiles()
        {
            var dataAccess = new FileDataAccess();

            return await dataAccess.GetAllFilesAsync();
        }
    }
}