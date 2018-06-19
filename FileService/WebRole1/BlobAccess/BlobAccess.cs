using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage;

namespace WebRole1.BlobAccess
{
    public class BlobAccess : IBlobAccess
    {
        public async Task<bool> UploadFileAsync(HttpPostedFile file)
        {
            try
            {
                CloudStorageAccount storageAccount =
                    CloudStorageAccount.Parse(Microsoft.Azure.CloudConfigurationManager.GetSetting("ConnectionString"));
                var cloudTableClient = storageAccount.CreateCloudBlobClient();

                var container = cloudTableClient.GetContainerReference("filesContainer");
                await container.CreateIfNotExistsAsync();

                var blobReference = container.GetBlockBlobReference(file.FileName.ToLowerInvariant());
                if (blobReference.Exists())
                {
                    // Delete the file and upload new one
                    await blobReference.DeleteAsync();
                    await blobReference.UploadFromStreamAsync(file.InputStream);
                }
                else
                {
                    await blobReference.UploadFromStreamAsync(file.InputStream);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private async Task<HttpResponseMessage> DownloadFileAsync(string fileName)
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(Microsoft.Azure.CloudConfigurationManager.GetSetting("ConnectionString"));
            var cloudTableClient = storageAccount.CreateCloudBlobClient();

            var container = cloudTableClient.GetContainerReference("filesContainer");
            await container.CreateIfNotExistsAsync();

            var blobReference = container.GetBlockBlobReference(fileName.ToLowerInvariant());

            if (blobReference.Exists())
            {
                var stream = new MemoryStream();
                await blobReference.DownloadToStreamAsync(stream);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return result;
            }
            else
            {
                throw new Exception("Does not exist");
            }
        }
    }
}