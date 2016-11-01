using System;
using System.Collections;
using Builder.TimeCalendar;

namespace Builder.Equipment
{
    /// <summary>
    /// Implement IEquipment
    /// </summary>
    public class SingleEquipment : IEquipment
    {
        private int index;
        private Calendar ca; //��������� ��� �������� ������������
        private int eqid; //id ������������
        private bool occflag; //���� ��������� ������������; true - ��������; false - ������;
        private string individualName;
        //Dictionary<int, bool> eqtacts; //������� ������� ������; int - �������� ����; bool - �������� ��������� ������������ � ������� ���� 
        //(true - ��������, false - ������); �� ��������� ���� ��������, ��������� �� ������ ������� ��������� ���������
        private DateTime OccupyT1 = DateTime.MinValue;
        private DateTime OccupyT2 = DateTime.MinValue;

        public SingleEquipment(Calendar ca, int id, string individualName)
        {
            this.ca = ca;
            eqid = id;
            this.individualName = individualName;
        }

        /// <summary>
        /// �������� ��������� ������������ 
        /// </summary>   
        public Calendar GetCalendar()
        {
            return ca;
        }

        /// <summary>
        /// �������� id ������������ (���� ������������ ���������, �� ������������ id ������ ������������)
        /// </summary>      
        public int GetID()
        {
            return eqid;
        }

        /// <summary>
        /// �������� ����������� ������������ � ���� ������� T
        /// true - ������������ ��������; false - ������
        /// </summary>        
        public bool IsNotOccupied(DateTime T)
        {
            return OccupyT2 <= T;
        }

        /// <summary>
        /// ������ ������������ c t1 �� t2
        /// </summary>  
        public void OccupyEquip(DateTime t1, DateTime t2)
        {
            //GetCalendar().OccupyHours(t1, t2);
            OccupyT1 = t1;
            OccupyT2 = t2;

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
    }
}
