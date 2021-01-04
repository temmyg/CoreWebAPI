using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApi.Services
{
    public class RepoService : IRepoService
    {
        public int GetDevicesCount()
        {
            return 15;
        }
    }
}
