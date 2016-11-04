using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder.Equipment
{
    public interface IEquipment : IEnumerable, IEnumerator
    {
        int GetID();
        Calendar GetCalendar();
        //string ToString();
        //bool IsOccupied(DateTime T);
        //void OccupyEquip(DateTime t1, DateTime t2);
    }
}
