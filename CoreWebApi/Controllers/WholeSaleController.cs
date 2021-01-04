using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholeSaleController : ControllerBase
    {   
        public WholeSaleController(IBankAccount vendorAccount)
        {
            vendorAccount.AccountNo = "2r3ir0q8wreqwr023";
        }

        [Route("is-active-invoice")]
        public ActionResult<FillingItem> CheckIsActiveInvoice(/*[FromBody]*/int invoiceId)
        {
            //return new FillingItem();

            if(invoiceId == 32146)
            {   
                return Ok(new { hasBeenShipped = true, DeliveryTime = "11-9/2020" });
            }
            else
            {
                return Ok(new { hasBeenShipped = false, DeliveryTime = "" });
            }
        }

        [Route("in-replenish-items")]
        public IActionResult InReplenishItem()
        {
            FillingItem[] items =
            {
                new FillingItem(){ ItemId = 21, ItemName = "GameStation", Number = 21, VendorId = 13},
                new FillingItem(){ ItemId = 33, ItemName = "iWatch", Number=402, VendorId = 37 },
                new FillingItem(){ ItemId = 131, ItemName = "Wood Collector", Number=31, VendorId = 17 }
            };

            return Ok(items);
        }
    }
}