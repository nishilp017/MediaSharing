using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.DataAccess
{
    public class FileEntity : TableEntity
    {
        public FileEntity()
        {
        }

        public FileEntity(string fileName, string fileId, string contentLength)
        {
            this.PartitionKey = fileId;
            this.RowKey = fileName;
            this.ContentLength = contentLength;
        }

        public string ContentLength { get; set; }
    }
}