using System.Collections.Generic;
using Debugger.Exceptions;
using Builder;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionTimePeriod : IExceptionSearch
    {
        List<Decision> decisions;

        public ExceptionTimePeriod(List<Decision> decisions)
        {
            // Конструктор
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
                                                 "Время начала операции : " + decision.GetStartTime().ToString(),
                                                 "Время окончания операции : " + decision.GetEndTime().ToString()));
                }
            }

            return exceptions;
        }
    }
}
