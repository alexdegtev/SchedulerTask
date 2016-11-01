using System.Collections.Generic;
using Debugger.Exceptions;

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
