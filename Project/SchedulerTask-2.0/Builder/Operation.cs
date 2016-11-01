using Builder.Equipment;
using System;
using System.Collections.Generic;

namespace Builder
{
        public interface IOperation
        {
            TimeSpan GetDuration();
            void SetOperationInPlan(DateTime realStartTime, DateTime realEndTime, SingleEquipment realEquipmentId);
            bool IsEnd(DateTime time);
            bool IsEnabled();
            bool PreviousOperationIsEnd(DateTime time);
            IEquipment GetEquipment();
            Party GetParty();
            Decision GetDecision();
            List<IOperation> GetPrevOperations();
            int GetId();
        }

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
            private Decision decision = null;//decision for this operation
            private Party parentParty;//link to parent party for this operation
            
            public Operation(int id, string name, TimeSpan duration, List<IOperation> previous, IEquipment equipment, Party party)
            {
                this.id = id;
                this.name = name;
                this.duration = duration;
                previousOperations = new List<IOperation>();
                foreach (IOperation prev in previous)
                {
                    previousOperations.Add(prev);
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
                decision = new Decision(realStartTime, realEndTime, realEquipmentId, this);
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
            public bool IsEnd(DateTime time)
            {
                bool end = false;
                if (IsEnabled())
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
                return parentParty;
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
                return previousOperations;
            }
    } 
}
