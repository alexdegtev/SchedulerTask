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
    class ExceptionInvalidStartDate : IExceptionSearch
    {
        public ExceptionInvalidStartDate()
        {
            // Конструктор
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в сформированных начальных датах...");
            List<IException> exceptions = new List<IException>();

            return exceptions;
        }
    }
}
