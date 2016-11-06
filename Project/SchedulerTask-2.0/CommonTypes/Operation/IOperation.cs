using System;
using System.Collections.Generic;
using CommonTypes.Equipment;
using CommonTypes.Party;
using CommonTypes.Decision;

namespace CommonTypes.Operation
{
    public interface IOperation
    {
        TimeSpan GetDuration();
        void SetOperationInPlan(DateTime realStartTime, DateTime realEndTime, SingleEquipment realEquipmentId);
        bool IsEnd(DateTime time);
        bool IsEnabled();
        bool PreviousOperationIsEnd(DateTime time);
        IEquipment GetEquipment();
        IParty GetParty();
        IDecision GetDecision();
        List<IOperation> GetPrevOperations();
        int GetId();
        void AddPrevOperation(IOperation op);
        string GetName();
    }
}
