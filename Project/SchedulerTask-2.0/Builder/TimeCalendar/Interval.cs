using System;

namespace Builder.TimeCalendar
{
    public class Interval : IComparable
    {
        private DateTime startTime;
        private DateTime endTime;

        public Interval(DateTime starttime, DateTime endtime)
        {
            this.startTime = starttime;
            this.endTime = endtime;
        }

        public DateTime GetStartTime()
        { return startTime; }

        public DateTime GetEndTime()
        { return endTime; }

        public void SetStartTime(DateTime val)
        {
            startTime = val;
        }

        public void SetEndTime(DateTime val)
        {
            endTime = val;
        }

        public int CompareTo(object obj)
        {
            Interval i2 = obj as Interval;
            if (obj == null) return 0;

            return DateTime.Compare(startTime, i2.startTime);
        }
    }
}
