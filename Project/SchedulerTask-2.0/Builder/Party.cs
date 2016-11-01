using System;
using System.Collections.Generic;

namespace Builder
{
    /// <summary>
    /// Class for partys.
    /// </summary>
    public class Party
    {
        //список операций, необходимых для выполнения партии(заказа)
        private List<IOperation> operationsForParty;
        //ранне время начала
        private DateTime startTimeParty;
        //директивный срок(познее время окончания)
        private DateTime endTimeParty;
        //приоритет
        private int priority;
        //name партии
        private String name;
        //num продукта
        private int numProducts;
        //родитель(в случае с подпартиями - деревья)
        private Party parent;
        //подпартии
        private List<Party> subParty;

        private TreeIterator iterator;


        /// <summary>
        /// Конструктор для партий.
        /// </summary>
        /// <param name="startTime"> Ранне время начала.</param>
        /// <param name="endTime"> Директивный срок(познее время окончания).</param>
        /// <param name="priority"> Приоритет. </param>
        /// <param name="name"> Название партии. </param>
        /// <param name="numProducts"> Номер партии. </param>
        public Party(DateTime startTime, DateTime endTime, int priority, string name, int numProducts)
        {
            this.startTimeParty = startTime;
            this.endTimeParty = endTime;
            this.priority = priority;
            this.name = name;
            this.numProducts = numProducts;
            operationsForParty = new List<IOperation>();
            subParty = new List<Party>();

        }

        //конструктор для подпартий
        public Party(string name, int numProducts)
        {
            this.name = name;
            this.numProducts = numProducts;
            subParty = new List<Party>();
        }

        public Party()
        {
        }

        //добавлениее операций партии
        public void AddOperationToForParty(IOperation operation)
        {
            if (operationsForParty == null)
            {
                operationsForParty = new List<IOperation>();
            }
            operationsForParty.Add(operation);

        }

        public void AddSubPArty(Party subPart)
        {
            if (subParty == null)
            {
                subParty = new List<Party>();
            }
            subParty.Add(subPart);

            if (subPart.GetParent() == null)
            {
                subPart.SetParent(this);
            }
        }

        public Party GetParent()
        {
            return parent;
        }

        public void SetParent(Party parent)
        {
            this.parent = parent;
        }

        public Party GetRoot()
        {
            if (parent == null)
            {
                return null;
            }
            else
            {
                Party tmp = this;
                while (true)
                {
                    if (tmp.GetParent() == null)
                    {
                        // tmp = this;
                        break;
                    }
                    else
                    {
                        tmp = tmp.GetParent();
                    }
                }
                return tmp;
            }
        }

        public TreeIterator GetIterator()
        {
            if (iterator == null)
            {
                return new TreeIterator(GetRoot());
            }
            return iterator;

        }

        public TreeIterator GetIterator(Party aRoot)
        {
            if (iterator == null)
            {
                return new TreeIterator(aRoot);
            }
            return iterator;

        }

        public void SetStartTimeParty(DateTime start)
        {
            this.startTimeParty = start;
        }

        public void SetEndTimeParty(DateTime end)
        {
            this.endTimeParty = end;
        }

        public void SetPriority(int pr)
        {
            this.priority = pr;
        }

        public int GetPriority()
        {
            return priority;
        }

        public DateTime GetStartTimeParty()
        {
            return startTimeParty;
        }

        public DateTime GetEndTimeParty()
        {
            return endTimeParty;
        }

        public string GetPartyName()
        {
            return name;
        }

        public void SetPartyName(string name)
        {
            this.name = name;
        }

        public void SetNumProducts(int num)
        {
            numProducts = num;
        }

        public int GetNumProducts()
        {
            return numProducts;
        }

        public List<IOperation> GetPartyOperations()
        {
            return operationsForParty;
        }

        public List<Party> GetSubParty()
        {
            return subParty;
        }

    }
}
