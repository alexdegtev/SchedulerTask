using System;
using System.Collections.Generic;
using CommonTypes.Equipment;
using CommonTypes.Decision;
using CommonTypes.Party;

namespace CommonTypes.Operation
{
    /// <summary>
    /// Class for operations
    /// </summary>   
    public class Operation : IOperation
    {
        private int id;//id of operation
        private string name;//name of operatiion
        private TimeSpan duration;//duration of operation
        private List<IOperation> previousOperations;//list of previous operations fo this operation
        private bool enable;//flag, if operation in plan-true, else - false
        private IEquipment equipment;//equipment or group of equipment for this operation
        private IDecision decision = null;//decision for this operation
        private IParty parentParty;//link to parent party for this operation

        public Operation(int id, string name, TimeSpan duration, List<IOperation> prev, IEquipment equipment, IParty party)
        {
            this.id = id;
            this.name = name;
            this.duration = duration;
            previousOperations = new List<IOperation>();
            foreach (IOperation operation in prev)
            {
                previousOperations.Add(operation);
            }
            enable = false;
            this.equipment = equipment;
            parentParty = party;
        }

        /// <summary>
        /// Get id of operation
        /// </summary>   
        public int GetId()
        {
            return id;
        }

        /// <summary>
        /// Get name of operatiion
        /// </summary>   
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Get duration of operation
        /// </summary>   
        public TimeSpan GetDuration()
        {
            return duration;
        }

        /// <summary>
        /// Set operation in plan
        /// </summary>   
        public void SetOperationInPlan(DateTime realStartTime, DateTime realEndTime, SingleEquipment realEquipmentId)
        {
            enable = true;
            decision = new Decision.Decision(realStartTime, realEndTime, realEquipmentId, this);
        }

        /// <summary>
        /// Function return:if operation in plan-true, else - false
        /// </summary>  
        public bool IsEnabled()
        {
            return enable;
        }

        /// <summary>
        /// Выполнилась ли операция к тому времени,которое подано на вход
        /// </summary>  
        public bool IsEnd(DateTime time)
        {
            bool end = false;
            if (this.IsEnabled())
            {
                if (time >= decision.GetEndTime())
                {
                    end = true;
                }
            }
            return end;
        }

        /// <summary>
        /// выполнены ли предыдущие операции
        /// </summary>  
        public bool PreviousOperationIsEnd(DateTime time)
        {
            bool flag = true;
            foreach (IOperation prev in previousOperations)
            {
                if (prev.IsEnd(time) == false)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// Получить оборудование или группу оборудований, на котором может выполняться операция
        /// </summary>  
        public IEquipment GetEquipment()
        {
            return equipment;
        }

        /// <summary>
        /// Получить ссылку на партию,в которой состоит данная операция
        /// </summary>   
        public IParty GetParty()
        {
            return parentParty;
        }

        /// <summary>
        /// Получить ссылку решение для данной операции
        /// </summary>
        public IDecision GetDecision()
        {
            return decision;
        }

        /// <summary>
        /// Получить ссылку на список предыдущих операций
        /// </summary>
        public List<IOperation> GetPrevOperations()
        {
            return previousOperations;
        }

        public void AddPrevOperation(IOperation op)
        {
            previousOperations.Add(op);
        }
          
        public override string ToString()
        {
            if (previousOperations.Count == 0)
                return String.Format("<Operation id=\"{0}\" name=\"{1}\" state=\"NOTSCHEDULED\" duration=\"{2}\" equipmentgroup=\"{3}\" />",
                id, name, duration, equipment.GetId());

            else
            {
                string operationInfo = String.Format("<Operation id=\"{0}\" name=\"{1}\" state=\"NOTSCHEDULED\" duration=\"{2}\" equipmentgroup=\"{3}\"",
                id, name, duration, equipment.GetId());

                foreach (IOperation o in previousOperations)
                    operationInfo += "\n<Previous id=" + o.GetId() + "/>";

                operationInfo += "\n</Operation>";
                return operationInfo;
            }

        }
    }
}
