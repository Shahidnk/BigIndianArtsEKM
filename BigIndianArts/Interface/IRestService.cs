using BigIndianArts.Models;
using Flurl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigIndianArts.Interface
{
    public interface IRestService
    {
        Task<T> GET<T>(Url url);

        Task<T> POST<T>(Url url, object data);

        Task<T> POSTFILE<T>(Url url, string ModuleName, FileToUpload file);
       

        Task<T> DELETE<T>(Url url);
    }
}
