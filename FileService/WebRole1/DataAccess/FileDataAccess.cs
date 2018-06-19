using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebRole1.DataAccess
{
    public class FileDataAccess : IFileDataAccess
    {
        public async Task<string> WriteFileAsync(FileEntity entity)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Microsoft.Azure.CloudConfigurationManager.GetSetting("ConnectionString"));
            var cloudTableClient = storageAccount.CreateCloudTableClient();

            var filesTable = cloudTableClient.GetTableReference("files");

            var operation = TableOperation.InsertOrReplace(entity);
            var result = await filesTable.ExecuteAsync(operation);
            return entity.PartitionKey;
        }
    }
}