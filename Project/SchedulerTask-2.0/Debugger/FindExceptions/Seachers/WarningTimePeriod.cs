using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes;
using CommonTypes.Operation;
using CommonTypes.Decision;

namespace Debugger.FindExceptions.Seachers
{
    class WarningTimePeriod : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<IDecision> decisions;
        // Конструктор
        public WarningTimePeriod(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем нарушения директивных сроков...");
            List<IException> exceptions = new List<IException>();
           
            foreach (var decision in decisions)
            {
                if (decision.GetEndTime() < operations[decision.GetOperation().GetId()].GetParty().GetEndTimeParty())
                {
                    
                    exceptions.Add(new Exception("D00",
                                                 "Warning",
                                                 "В построенном расписании имеется нарушение директивного срока",
                                                 decision.ToString(),
                                                 ""));
                }
            }

            
            return exceptions;
        }
    }
}
