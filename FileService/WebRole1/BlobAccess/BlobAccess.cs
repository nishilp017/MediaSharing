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

                var container = cloudTableClient.GetContainerReference("filescontainer");
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

        public async Task<HttpResponseMessage> DownloadFileAsync(string fileName)
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(Microsoft.Azure.CloudConfigurationManager.GetSetting("ConnectionString"));
            var cloudTableClient = storageAccount.CreateCloudBlobClient();

            var container = cloudTableClient.GetContainerReference("filescontainer");
            await container.CreateIfNotExistsAsync();

            var blobReference = container.GetBlockBlobReference(fileName.ToLowerInvariant());

            if (blobReference.Exists())
            {
                var stream = await blobReference.OpenReadAsync();

                var result =
                    new HttpResponseMessage(HttpStatusCode.OK) {Content = new StreamContent(stream)};

                result.Content.Headers.ContentLength = blobReference.Properties.Length;
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName,
                    Size = blobReference.Properties.Length
                };

                result.Content.Headers.ContentType = new MediaTypeHeaderValue(blobReference.Properties.ContentType);
                return result;
            }
            else
            {
                throw new Exception("Does not exist");
            }
        }
    }
}