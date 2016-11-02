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
    class GroupEquipment : IEquipment
    {
        int index;
        List<IEquipment> equiplist;
        int eqid; //id ������������
        string name;

        public GroupEquipment(Calendar ca, int id, string name)
        {
            eqid = id;
            this.name = name;
            equiplist = new List<IEquipment>();
        }

        public void AddEquipment(IEquipment e)
        {
            equiplist.Add(e);
        }


        ///// <summary>
        ///// ������ ������������ c t1 �� t2
        ///// </summary>  
        //public void OccupyEquip(DateTime t1, DateTime t2)
        //{
        //    equiplist[index].GetCalendar().OccupyHours(t1, t2);
        //}

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
            foreach (SingleEquipment e in this)
            {
                if (e.IsNotOccupied(T)) return true;
            }

            return false;
        }


        public override string ToString()
        {
            return String.Format("<EquipmentGroup id=\"{0}\" name=\"{1}\" workingmode=\"INTERUPTIONS\" />",
                eqid, name);
        }


        public IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }

        public bool MoveNext()
        {
            bool res = equiplist[index].MoveNext();
            if (!res)
            {
                index++;

                if (index == equiplist.Count)
                {
                    return false;
                }

                equiplist[index].MoveNext();
            }

            return true;
        }

        public void Reset()
        {
            index = 0;
            foreach (IEquipment e in equiplist)
                e.Reset();
        }

        public object Current
        {
            get
            {
                return equiplist[index].Current;
            }
        }
    }
}
