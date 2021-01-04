using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApi.Model
{
    interface INavigationService
    {
        string GetLoginPath();

        //string GetDeviceHomePath();
    }
}
