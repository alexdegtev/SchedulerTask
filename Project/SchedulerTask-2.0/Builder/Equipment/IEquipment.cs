using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Equipment
{
    public interface IEquipment : IEnumerable, IEnumerator
    {
        int GetID();
        //string ToString();
        //bool IsOccupied(DateTime T);
        //void OccupyEquip(DateTime t1, DateTime t2);
    }
}
