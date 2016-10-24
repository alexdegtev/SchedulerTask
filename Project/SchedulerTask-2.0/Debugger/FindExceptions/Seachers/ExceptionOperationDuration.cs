//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debugger;
using Debugger.Exceptions;
using Builder;
using Builder.Equipment;

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
