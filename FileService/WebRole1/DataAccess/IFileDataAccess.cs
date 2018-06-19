using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebRole1.DataAccess
{
    public interface IFileDataAccess
    {
        Task<string> WriteFileAsync(FileEntity entity);
    }
}