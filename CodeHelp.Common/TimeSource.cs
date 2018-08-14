using System;

namespace CodeHelp.Common
{
    public class TimeSource : ITimeSource
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}