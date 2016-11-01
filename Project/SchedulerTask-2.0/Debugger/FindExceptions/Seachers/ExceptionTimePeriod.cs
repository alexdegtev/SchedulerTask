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
        public ExceptionTimePeriod()
        {
            // Конструктор
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в датах операций...");
            List<IException> exceptions = new List<IException>();

            return exceptions;
        }
    }
}
