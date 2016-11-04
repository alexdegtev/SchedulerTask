//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Debugger;
using Debugger.Exceptions;
using Builder;
using Builder.Equipment;

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
                                                 " ",
                                                 decision.ToString()));
                }
            }

            return exceptions;
        }
    }
}
