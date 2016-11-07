using System;
using System.Collections.Generic;

namespace CommonTypes.Calendar
{
    public interface ICalendar
    {
        void AddIntervals(List<Interval> intervallist);
        bool IsInterval(DateTime T, out int intervalindex);
        bool WillRelease(DateTime T, TimeSpan t, int intervalindex);
        int FindInterval(DateTime T, out bool flag);
        DateTime GetTimeofRelease(DateTime T, TimeSpan t, int intervalindex);
        DateTime GetNearestStart(DateTime T);
        Interval GetInterval(int index);
        TimeSpan GetTimeInTwentyFourHours();
    }
}
