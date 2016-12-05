using System.Collections.Generic;
using System;
using Debugger.Exceptions;
using CommonTypes.Decision;
using CommonTypes.Operation;
using CommonTypes;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionOperationDuration : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<IDecision> decisions;

        public ExceptionOperationDuration(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            // Конструктор
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в длительностях операций...");
            List<IException> exceptions = new List<IException>();
            bool inFirstInterval = false;
            bool inLastInterval  = false;

            foreach(var decision in decisions)
            {
                TimeSpan realtime = decision.GetEquipment().GetCalendar().GetRealWorkTime(
                    decision.GetStartTime(), decision.GetEndTime(), out inFirstInterval, out inLastInterval);
                TimeSpan duration = decision.GetOperation().GetDuration();
                
                if(realtime < duration)
                {
                    exceptions.Add(new Debugger.Exceptions.Exception("V03",
                                                 "Error",
                                                 "Несоответствие набора исходных операций операциям в расписании",
                                                 "",
                                                 ""));
                }
            }

            return exceptions;
        }
    }
}
