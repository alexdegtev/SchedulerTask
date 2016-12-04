using System;
using System.Collections.Generic;
using CommonTypes.Operation;

namespace CommonTypes.Party
{
    /// <summary>
    /// Class for partys.
    /// </summary>
    public class Party : IParty
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
        private IParty parent;
        //подпартии
        private List<IParty> subParty;

        private TreePartyIterator iterator = null;


        /// <summary>
        /// Конструктор для партий.
        /// </summary>
        /// <param name="startTime">Ранне время начала</param>
        /// <param name="endTime">Директивный срок (познее время окончания)</param>
        /// <param name="priority">Приоритет</param>
        /// <param name="name">Название партии</param>
        /// <param name="numProducts">Номер партии</param>
        public Party(DateTime startTime, DateTime endTime, int priority, String name, int numProducts)
        {
            this.startTimeParty = startTime;
            this.endTimeParty = endTime;
            this.priority = priority;
            this.name = name;
            this.numProducts = numProducts;
            operationsForParty = new List<IOperation>();
            subParty = new List<IParty>();
        }

        //конструктор для подпартий
        public Party(String name, int numProducts)
        {
            this.name = name;
            this.numProducts = numProducts;
            subParty = new List<IParty>();
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

        public void AddSubParty(IParty subPart)
        {
            if (subParty == null)
            {
                subParty = new List<IParty>();
            }
            subParty.Add(subPart);

            if (subPart.Parent == null)
            {
                subPart.Parent = this;
            }
        }

        public IParty GetRoot()
        {
            if (this.parent == null)
            {
                return null;
            }
            else
            {
                IParty tmp = this;
                while (true)
                {
                    if (tmp.Parent == null)
                    {
                        // tmp = this;
                        break;
                    }
                    else
                    {
                        tmp = tmp.Parent;
                    }
                }
                return tmp;
            }
        }

        public TreePartyIterator GetIterator()
        {
            if (iterator == null)
            {
                return new TreePartyIterator(GetRoot());
            }
            return iterator;

        }

        public TreePartyIterator GetIterator(IParty aRoot)
        {
            if (iterator == null)
            {
                return new TreePartyIterator(aRoot);
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
            this.numProducts = num;
        }
        public int GetNumProducts()
        {
            return numProducts;
        }

        public List<IOperation> GetPartyOperations()
        {
            return operationsForParty;
        }

        public List<IParty> GetSubParty()
        {
            return subParty;
        }



        public IParty Parent
        {
            get { return parent; }
        }

        IParty IParty.Parent
        {
            get { return parent; }
            set { parent = value; }
        }
    }
}
