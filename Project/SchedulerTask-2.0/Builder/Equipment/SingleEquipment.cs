using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace Builder.Equipment
{
    /// <summary>
    /// Implement IEquipment
    /// </summary>
    public class SingleEquipment : IEquipment
    {
        int index;
        Calendar ca; //��������� ��� �������� ������������
        int eqid; //id ������������
        bool occflag; //���� ��������� ������������; true - ��������; false - ������;
        string individualname;
        //Dictionary<int, bool> eqtacts; //������� ������� ������; int - �������� ����; bool - �������� ��������� ������������ � ������� ���� 
        //(true - ��������, false - ������); �� ��������� ���� ��������, ��������� �� ������ ������� ��������� ���������
        DateTime OccupyT1 = DateTime.MinValue;
        DateTime OccupyT2 = DateTime.MinValue;

        public SingleEquipment(Calendar ca, int id, string individualname)
        {
            this.ca = ca;
            eqid = id;
            this.individualname = individualname;
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


        public override string ToString()
        {
            return String.Format("<Equipment id=\"{0}\" name=\"{1}\" />",
                eqid, individualname);
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
