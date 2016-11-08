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
        ICalendar ca; //��������� ��� �������� ������������
        int eqId; //id ������������
        //bool occFlag; //���� ��������� ������������; true - ��������; false - ������;
        string individualName;
        //Dictionary<int, bool> eqtacts; //������� ������� ������; int - �������� ����; bool - �������� ��������� ������������ � ������� ���� 
        //(true - ��������, false - ������); �� ��������� ���� ��������, ��������� �� ������ ������� ��������� ���������
        DateTime occupyT1 = DateTime.MinValue;
        DateTime occupyT2 = DateTime.MinValue;

        public SingleEquipment(ICalendar ca, int id, string individualname)
        {
            this.ca = ca;
            eqId = id;
            this.individualName = individualname;
        }

        /// <summary>
        /// �������� ��������� ������������ 
        /// </summary>   
        public ICalendar GetCalendar()
        {
            return ca;
        }

        /// <summary>
        /// �������� id ������������ (���� ������������ ���������, �� ������������ id ������ ������������)
        /// </summary>      
        public int GetId()
        {
            return eqId;
        }

        /// <summary>
        /// �������� ����������� ������������ � ���� ������� T
        /// true - ������������ ��������; false - ������
        /// </summary>        
        public bool IsNotOccupied(DateTime T)
        {
            return occupyT2 <= T;
        }

        /// <summary>
        /// ������ ������������ c t1 �� t2
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
