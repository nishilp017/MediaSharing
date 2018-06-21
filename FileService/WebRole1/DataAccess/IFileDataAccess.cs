using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebRole1.DataAccess
{
    public interface IFileDataAccess
    {
        Task<string> WriteFileAsync(FileEntity entity);

        Task<TableEntity> GetFileAsync(string fileId);

        Task<IEnumerable<FileEntity>> GetAllFilesAsync();
    }
}