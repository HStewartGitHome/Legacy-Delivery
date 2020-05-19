using System;

namespace DeliverySupport.Services
{
    public interface IDataCleanup
    {
        int GetCleanupHours(string str);
        DateTime GetPreviousDate(string str);
    }
}