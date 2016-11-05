using Builder.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Builder
{
    public interface IOperation
    {
        TimeSpan GetDuration();
        void SetOperationInPlan(DateTime real_start_time, DateTime real_end_time, SingleEquipment real_equipment_id);
        bool IsEnd(DateTime time_);
        bool IsEnabled();
        bool PreviousOperationIsEnd(DateTime time_);
        IEquipment GetEquipment();
        Party GetParty();
        Decision GetDecision();
        List<IOperation> GetPrevOperations();
        int GetID();
        void AddPrevOperation(IOperation op);
    }

    /// <summary>
    /// Class for operations
    /// </summary>   
    public class Operation : IOperation
    {
        private int id;//id of operation
        private string name;//name of operatiion
        private TimeSpan duration;//duration of operation
        private List<IOperation> PreviousOperations;//list of previous operations fo this operation
        private bool enable;//flag, if operation in plan-true, else - false
        private IEquipment equipment;//equipment or group of equipment for this operation
        private Decision decision = null;//decision for this operation
        private Party parent_party;//link to parent party for this operation

        public Operation(int id_, string name_, TimeSpan duration_, List<IOperation> Prev, IEquipment equipment_, Party party)
        {
            id = id_;
            name = name_;
            duration = duration_;
            PreviousOperations = new List<IOperation>();
            foreach (IOperation prev in Prev)
            {
                PreviousOperations.Add(prev);
            }
            enable = false;
            equipment = equipment_;
            parent_party = party;
        }

        /// <summary>
        /// Get id of operation
        /// </summary>   
        public int GetID()
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
        public void SetOperationInPlan(DateTime real_start_time, DateTime real_end_time, SingleEquipment real_equipment_id)
        {
            enable = true;
            decision = new Decision(real_start_time, real_end_time, real_equipment_id, this);
        }

        /// <summary>
        /// function return:if operation in plan-true, else - false
        /// </summary>  
        public bool IsEnabled()
        {
            return enable;
        }

        /// <summary>
        /// выполнилась ли операция к тому времени,которое подано на вход
        /// </summary>  
        public bool IsEnd(DateTime time_)
        {
            bool end = false;
            if (this.IsEnabled())
            {
                if (time_ >= decision.GetEndTime())
                {
                    end = true;
                }
            }
            return end;
        }

        /// <summary>
        /// выполнены ли предыдущие операции
        /// </summary>  
        public bool PreviousOperationIsEnd(DateTime time_)
        {
            bool flag = true;
            foreach (IOperation prev in PreviousOperations)
            {
                if (prev.IsEnd(time_) == false)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// получить оборудование или группу оборудований, на котором может выполняться операция
        /// </summary>  
        public IEquipment GetEquipment()
        {
            return equipment;
        }

        /// <summary>
        /// получить ссылку на партию,в которой состоит данная операция
        /// </summary>   
        public Party GetParty()
        {
            return parent_party;
        }


        /// <summary>
        /// получить ссылку решение для данной операции
        /// </summary>
        public Decision GetDecision()
        {
            return decision;
        }

        /// <summary>
        /// получить ссылку на список предыдущих операций
        /// </summary>
        public List<IOperation> GetPrevOperations()
        {
            return PreviousOperations;
        }

        public void AddPrevOperation(IOperation op)
        {
            PreviousOperations.Add(op);
        }
          
        public override string ToString()
            {
                if (PreviousOperations.Count == 0)
                    return String.Format("<Operation id=\"{0}\" name=\"{1}\" state=\"NOTSCHEDULED\" duration=\"{2}\" equipmentgroup=\"{3}\" />",
                    id, name, duration, equipment.GetID());

                else
                {
                    string operation_info = String.Format("<Operation id=\"{0}\" name=\"{1}\" state=\"NOTSCHEDULED\" duration=\"{2}\" equipmentgroup=\"{3}\"",
                    id, name, duration, equipment.GetID());

                    foreach (Operation o in PreviousOperations)
                        operation_info += "\r\n<Previous id=" + o.GetID() + "/>";

                    operation_info += "\r\n </Operation>";
                    return operation_info;
                }

            }
        }
    

}
