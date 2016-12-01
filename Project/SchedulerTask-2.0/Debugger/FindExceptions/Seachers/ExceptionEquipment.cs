using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionEquipment : IExceptionSearch
    {
        public ExceptionEquipment()
        {
            // Конструктор
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в соответствии оборудования операциям...");
            List<IException> exceptions = new List<IException>();

            return exceptions;
        }
    }
}
