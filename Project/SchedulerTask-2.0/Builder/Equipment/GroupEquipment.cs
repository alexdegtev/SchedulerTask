using System;
using System.Collections.Generic;
using System.Collections;
using Builder.TimeCalendar;

namespace Builder.Equipment
{
    /// <summary>
    /// Implement IEquipment
    /// </summary>
    class GroupEquipment : IEquipment
    {
        private int index;
        private List<IEquipment> equipList;
        private int eqId; //id ������������
        private string name;

        public GroupEquipment(Calendar ca, int id, string name)
        {
            eqId = id;
            this.name = name;
            equipList = new List<IEquipment>();
        }

        public void AddEquipment(IEquipment e)
        {
            equipList.Add(e);
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
            return eqId;
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
    }
}
