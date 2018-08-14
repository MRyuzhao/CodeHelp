using System;

namespace CodeHelp.Common
{
    public interface ITimeSource
    {
        DateTime GetCurrentTime();
    }
}