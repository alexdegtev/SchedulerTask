using Builder.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    /// <summary>
    /// Implement IOperation
    /// </summary>
    public class Operation
    {

    }
}
