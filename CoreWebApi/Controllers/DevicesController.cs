using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IRepoService repoService;
        private readonly IServiceProvider serviceProvider;
        private readonly string deviceIMEI = "FASF92322";
        private const string deviceIdPrefix = "amtgin";
        public DevicesController(HostBuilderContext hostBuilderContext, IHostLifetime hostLifetime, IHostEnvironment hostEnvironment, 
            IServiceProvider serviceProvider, IRepoService repoService, IEnumerable<FillingItem> fillingItems, IEnumerable<IBankAccount> bankAccounts)
        {
            this.repoService = repoService;
            this.serviceProvider = serviceProvider;
            deviceIMEI = "fasdf23e3";

            bankAccounts.ToList()[0].AccountNo = "99831";
        }

        [Route("devicesnum")]
        public int GetDevicesCount()
        {
            IItemFinder finder = this.serviceProvider.GetService<IItemFinder>();
            finder.SearchItem = "2FASIDF298";

            /*
             * Scoped life time, in the same request, the AccountNo = "99831" is persisting
             */
            IEnumerable<IBankAccount> accounts = this.serviceProvider.GetServices<IBankAccount>();
            accounts = (IEnumerable<IBankAccount>)HttpContext.RequestServices.GetServices(typeof(IBankAccount));

            /*
             * since it is transient life time, finder.SearchItem is null below
             */
            finder = this.serviceProvider.GetService<IItemFinder>();

            return repoService.GetDevicesCount();
        }

        [Route("groupsnum")]
        public int GetDeviceGroupsCount()
        {
            return 13;
        }

        [Route("serialno")]
        public ActionResult<string> SerialNo(string id)
        {
            return Content("12313544");
        }
    }
}