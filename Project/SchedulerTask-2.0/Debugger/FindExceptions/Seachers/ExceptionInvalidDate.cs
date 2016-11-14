using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionInvalidDate : IExceptionSearch
    {
        public ExceptionInvalidDate()
        {
            // Конструктор
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в назначении времени выполнения операций...");
            List<IException> exceptions = new List<IException>();

            return exceptions;
        }
    }
}
