using System;
using Builder.Equipment;

namespace Builder
{
    public interface IDecision
    {
        DateTime GetStartTime();
        DateTime GetEndTime();
        SingleEquipment GetEquipment();
        Operation GetOperation();
    }

    /// <summary>
    /// Class for saved operation to decision
    /// </summary>
    public class Decision:IDecision
    {
        private DateTime startTime;//real start time of operation in plan
        private DateTime endTime;// real end time of operation in plan
        private SingleEquipment equipmentId;//real id of equipment for this operation in plan
        private Operation op;//operation for this decision

        public Decision(DateTime startTime, DateTime endTime, SingleEquipment equipmentId, Operation op)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.equipmentId = equipmentId;
            this.op = op;
        }

        /// <summary>
        /// Get real start time of operation in plan
        /// </summary>   
        public DateTime GetStartTime()
        {
            return startTime;
        }

        /// <summary>
        /// Get real end time of operation in plan
        /// </summary>   
        public DateTime GetEndTime()
        {
            return endTime;
        }

        /// <summary>
        /// Get real id of equipment for this operation in plan
        /// </summary>   
        public SingleEquipment GetEquipment()
        {
            return equipmentId;
        }

        /// <summary>
        /// Get operation for this decision
        /// </summary>   
        public Operation GetOperation()
        {
            return op;
        }
    }
}
