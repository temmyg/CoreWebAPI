using System;

namespace IMEIManager
{
    public class IMEIValidator
    {
        public string IsPrefixValid(string imei)
        {
            return $"{imei[0..5]} not Valid";           
        }

        public string DeviceActivatedTime(string imei)
        {
            return "11-09-2011";
        }
    }
}
