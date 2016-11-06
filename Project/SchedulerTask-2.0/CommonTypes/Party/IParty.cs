using System;
using System.Collections.Generic;
using CommonTypes.Operation;

namespace CommonTypes.Party
{
    public interface IParty
    {
        void AddOperationToForParty(IOperation operation);
        void AddSubParty(IParty subPart);
        IParty Parent { get; set; }
        IParty GetRoot();
        TreePartyIterator GetIterator();
        TreePartyIterator GetIterator(IParty aRoot);
        void SetStartTimeParty(DateTime start);
        void SetEndTimeParty(DateTime end);
        void SetPriority(int pr);
        int GetPriority();
        DateTime GetStartTimeParty();
        DateTime GetEndTimeParty();
        string GetPartyName();
        void SetPartyName(string name);
        void SetNumProducts(int num);
        int GetNumProducts();
        List<IOperation> GetPartyOperations();
        List<IParty> GetSubParty();
    }
}
