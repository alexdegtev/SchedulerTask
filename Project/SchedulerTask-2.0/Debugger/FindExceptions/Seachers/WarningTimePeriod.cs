using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes;

namespace Debugger.FindExceptions.Seachers
{
    class WarningTimePeriod : IExceptionSearch
    {
        // Конструктор
        public WarningTimePeriod()
        {
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем нарушения директивных сроков...");
            List<IException> exceptions = new List<IException>();

            return exceptions;
        }
    }
}
