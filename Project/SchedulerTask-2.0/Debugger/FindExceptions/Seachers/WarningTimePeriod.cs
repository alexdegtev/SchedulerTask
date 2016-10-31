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
    class WarningTimePeriod : IExceptionSearch
    {
        public WarningTimePeriod()
        {
            // Конструктор
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем нарушения директивных сроков...");
            List<IException> exceptions = new List<IException>();



            return exceptions;
        }
    }
}
