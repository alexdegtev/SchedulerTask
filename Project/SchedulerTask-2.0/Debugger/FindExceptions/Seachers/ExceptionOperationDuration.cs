using System.Collections.Generic;
using Debugger.Exceptions;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionOperationDuration : IExceptionSearch
    {
        public ExceptionOperationDuration()
        {
            // Конструктор
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в длительностях операций...");
            List<IException> exceptions = new List<IException>();

            return exceptions;
        }
    }
}
