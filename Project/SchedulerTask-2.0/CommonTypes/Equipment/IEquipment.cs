using System;
using System.Collections;
using CommonTypes.Calendar;

namespace CommonTypes.Equipment
{
    public interface IEquipment : IEnumerable, IEnumerator
    {
        int GetId();
        ICalendar GetCalendar();
        //string ToString();
        TimeSpan GetTimeWorkInTwentyFourHours();
        //bool IsOccupied(DateTime T);
        //void OccupyEquip(DateTime t1, DateTime t2);
    }
}
