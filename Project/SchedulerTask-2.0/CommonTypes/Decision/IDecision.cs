using System;
using CommonTypes.Equipment;
using CommonTypes.Operation;

namespace CommonTypes.Decision
{
    public interface IDecision
    {
        DateTime GetStartTime();
        DateTime GetEndTime();
        SingleEquipment GetEquipment();
        IOperation GetOperation();
    }
}
