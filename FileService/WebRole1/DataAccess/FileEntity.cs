using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.DataAccess
{
    public class FileEntity : TableEntity
    {
        public FileEntity(string fileName, string fileId)
        {
            this.PartitionKey = fileId;
            this.RowKey = fileName;
        }
    }
}