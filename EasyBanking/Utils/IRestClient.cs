using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBanking.Utils
{
    public interface IRestClient
    {
       Task<string> GetAsync(string url);
    }
}
