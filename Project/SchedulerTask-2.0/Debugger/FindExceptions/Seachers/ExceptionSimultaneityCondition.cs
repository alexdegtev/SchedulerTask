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
    class ExceptionSimultaneityCondition : IExceptionSearch
    {
        public ExceptionSimultaneityCondition()
        {
            // Конструктор
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Проверяем условие одновременного использования оборудования...");
            List<IException> exceptions = new List<IException>();

            return exceptions;
        }
    }
}
