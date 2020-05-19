using Microsoft.Extensions.Logging;
using System;

namespace DeliverySupport.Services
{

    public class DataCleanup : IDataCleanup
    {
        private ILogger<DataCleanup> _logger = null;

        public DataCleanup(ILogger<DataCleanup> logger)
        {
            _logger = logger;
        }

        public int GetCleanupHours(string str)
        {
            int Result = 12;

            if (!String.IsNullOrEmpty(str))
                Result = Convert.ToInt32(str);

            return Result;
        }

        public DateTime GetPreviousDate(string str)
        {
            int Hours = GetCleanupHours(str);
            return MakePreviousDate(Hours);
        }

        internal DateTime MakePreviousDate(int Hours)
        {
            DateTime time = DateTime.Now.AddHours(-Hours);

            return time;
        }

    }
}
