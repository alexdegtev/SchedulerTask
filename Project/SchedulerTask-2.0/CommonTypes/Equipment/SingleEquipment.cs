using System;
using System.Collections;
using CommonTypes.Calendar;

namespace CommonTypes.Equipment
{
    /// <summary>
    /// Implement IEquipment
    /// </summary>
    public class SingleEquipment : IEquipment
    {
        int index;
        ICalendar ca; //календарь для текущего оборудования
        int eqId; //id оборудования
        int parentGroupId;//id родительской группы оборудования(на 1 уровень выше)
        //bool occFlag; //флаг занятости оборудования; true - свободно; false - занято;
        string individualName;
        //Dictionary<int, bool> eqtacts; //словарь часовых тактов; int - значение часа; bool - значение занятости оборудования в течение часа 
        //(true - свободно, false - занято); по умолчанию весь интервал, состоящий из тактов времени считается свободным
        DateTime occupyT1 = DateTime.MinValue;
        DateTime occupyT2 = DateTime.MinValue;

        public SingleEquipment(ICalendar ca, int id,int parentId, string individualname)
        {
            this.ca = ca;
            eqId = id;
            parentGroupId = parentId;
            this.individualName = individualname;
        }

        /// <summary>
        /// получить календарь оборудования 
        /// </summary>   
        public ICalendar GetCalendar()
        {
            return ca;
        }

        /// <summary>
        /// получить id оборудования (если оборудование групповое, то возвращается id группы оборудования)
        /// </summary>      
        public int GetId()
        {
            return eqId;
        }

        /// <summary>
        /// получить id родительской группы оборудования(на 1 уровень выше)
        /// </summary>
        public int GetParentGroupId()
        {
            return parentGroupId;
        }
        /// <summary>
        /// проверка доступности оборудования в такт времени T
        /// true - оборудование доступно; false - занято
        /// </summary>        
        public bool IsNotOccupied(DateTime T)
        {
            return occupyT2 <= T;
        }

        /// <summary>
        /// Занять оборудование c t1 до t2
        /// </summary>  
        public void OccupyEquip(DateTime t1, DateTime t2)
        {
            //GetCalendar().OccupyHours(t1, t2);
            occupyT1 = t1;
            occupyT2 = t2;

        }

        public override string ToString()
        {
            return String.Format("Equipment id=\"{0}\" name=\"{1}\"",
                eqId, individualName);
        }
        
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (index == 1)
            {
                return false;
            }

            index++;
            return true;
        }

        public void Reset()
        {
            index = -1;
        }

        public object Current
        {
            get
            {
                return this;
            }
        }

        public TimeSpan GetTimeWorkInTwentyFourHours()
        {
            TimeSpan hours = ca.GetTimeInTwentyFourHours();
            return hours;
        }
    }
}
