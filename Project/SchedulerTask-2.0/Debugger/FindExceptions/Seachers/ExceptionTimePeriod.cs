using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes.Decision;
using CommonTypes;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionTimePeriod : IExceptionSearch
    {
        List<IDecision> decisions;

        // Конструктор
        public ExceptionTimePeriod(List<IDecision> decisions)
        {
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в датах операций...");
            List<IException> exceptions = new List<IException>();

            foreach (var decision in decisions)
            {
                if (decision.GetStartTime() > decision.GetEndTime())
                {
                    exceptions.Add(new Exception("R02",
                                                 "Error",
                                                 "Произошло нарушение временных характеристик",
                                                 "",
                                                 decision.ToString()));
                }
            }

            return exceptions;
        }
    }
}
