using System;
using System.Collections.Generic;
using System.Collections;
using CommonTypes.Calendar;

namespace CommonTypes.Equipment
{
    /// <summary>
    /// Implement IEquipment
    /// </summary>
    public class GroupEquipment : IEquipment
    {
        private int index;
        private List<IEquipment> equipList;
        private int eqId; //id оборудования
        private string name;

        public GroupEquipment(ICalendar ca, int id, string name)
        {
            eqId = id;
            this.name = name;
            equipList = new List<IEquipment>();
        }

        public ICalendar GetCalendar()
        {
            return null;
        }

        public void AddEquipment(IEquipment e)
        {
            equipList.Add(e);
        }

        ///// <summary>
        ///// Занять оборудование c t1 до t2
        ///// </summary>  
        //public void OccupyEquip(DateTime t1, DateTime t2)
        //{
        //    equiplist[index].GetCalendar().OccupyHours(t1, t2);
        //}

        /// <summary>
        /// получить id оборудования (если оборудование групповое, то возвращается id группы оборудования)
        /// </summary>      
        public int GetId()
        {
            return eqId;
        }

        /// <summary>
        /// проверка доступности оборудования в такт времени T
        /// true - оборудование доступно; false - занято
        /// </summary>        
        public bool IsNotOccupied(DateTime T)
        {
            foreach (SingleEquipment e in this)
            {
                if (e.IsNotOccupied(T)) return true;
            }

            return false;
        }

        public override string ToString()
        {
            return String.Format("EquipmentGroup id=\"{0}\" name=\"{1}\" workingmode=\"INTERUPTIONS\"",
                eqId, name);
        }

        public IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }

        public bool MoveNext()
        {
            bool res = equipList[index].MoveNext();
            if (!res)
            {
                index++;

                if (index == equipList.Count)
                {
                    return false;
                }

                equipList[index].MoveNext();
            }

            return true;
        }

        public void Reset()
        {
            index = 0;
            foreach (IEquipment e in equipList)
                e.Reset();
        }

        public object Current
        {
            get
            {
                return equipList[index].Current;
            }
        }

        public TimeSpan GetTimeWorkInTwentyFourHours()
        {
            TimeSpan hours = equipList[0].GetTimeWorkInTwentyFourHours();
            foreach (IEquipment i in equipList)
            {
                if (i.GetTimeWorkInTwentyFourHours() < hours)
                {
                    hours = i.GetTimeWorkInTwentyFourHours();
                }
            }
            return hours;
        }
    }
}
