using System;
using CommonTypes.Equipment;
using CommonTypes.Operation;

namespace CommonTypes.Decision
{
    /// <summary>
    /// Class for saved operation to decision
    /// </summary>
    public class Decision : IDecision
    {
        private DateTime startTime;//real start time of operation in plan
        private DateTime endTime;// real end time of operation in plan
        private SingleEquipment equipmentId;//real id of equipment for this operation in plan
        private IOperation op;//operation for this decision

        public Decision(DateTime startTime, DateTime endTime, SingleEquipment equipmentId, IOperation op)
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
        public IOperation GetOperation()
        {
            return op;
        }

        public override string ToString()
        {
            string isscheduled;
            if (GetOperation() != null) isscheduled = "SCHEDULED";
            else isscheduled = "NOTSCHEDULED";

            return String.Format("Operation id=\"{0}\" name=\"{1}\" state=\"{2}\" date_begin=\"{3}\" date_end=\"{4}\" equipment=\"{4}\" duration=\"{5}\"",
                equipmentId, GetOperation().GetName(), isscheduled, startTime, endTime, GetOperation().GetEquipment(), GetOperation().GetDuration());
        }
    }

}
