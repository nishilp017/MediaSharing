using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table.Queryable;

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

        public async Task<TableEntity> GetFileAsync(string fileId)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Microsoft.Azure.CloudConfigurationManager.GetSetting("ConnectionString"));
            var cloudTableClient = storageAccount.CreateCloudTableClient();

            var filesTable = cloudTableClient.GetTableReference("files");

            var operation = filesTable.CreateQuery<TableEntity>().Where(x => x.PartitionKey == fileId).AsTableQuery();

            var entries = await operation.ExecuteSegmentedAsync(null);

            var firstEntry = entries.FirstOrDefault();

            if(firstEntry == null)
                throw new Exception("Does not exist");

            return firstEntry;
        }

        public async Task<IEnumerable<FileEntity>> GetAllFilesAsync()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Microsoft.Azure.CloudConfigurationManager.GetSetting("ConnectionString"));
            var cloudTableClient = storageAccount.CreateCloudTableClient();

            var filesTable = cloudTableClient.GetTableReference("files");

            var files = new List<FileEntity>();
            TableContinuationToken token = null;
            TableQuery<FileEntity> query = new TableQuery<FileEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThan, String.Empty));

            do
            {
                TableQuerySegment<FileEntity> resultSegment = await filesTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                files.AddRange(resultSegment.Results);
            } while (token != null);

            return files;
        }
    }
}