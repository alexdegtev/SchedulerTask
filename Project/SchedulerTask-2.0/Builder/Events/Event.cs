using System;

namespace Builder.Events
{
    public class Event: IComparable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">Время возникновения события</param>
        public Event(DateTime time)
        {
            Time = time;
        }

        public DateTime Time { get; private set; }

        public int CompareTo(object obj)
        {
            Event e2 = obj as Event;
            if (e2 == null)
                return 0;

            return DateTime.Compare(Time, e2.Time);
        }
    }
}
