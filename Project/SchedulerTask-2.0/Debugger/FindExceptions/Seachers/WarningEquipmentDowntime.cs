using System.Collections.Generic;
using Debugger.Exceptions;

namespace Debugger.FindExceptions.Seachers
{
    class WarningEquipmentDowntime : IExceptionSearch
    {
        public WarningEquipmentDowntime()
        {
            // Конструктор
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем временные промежутки простоя оборудования...");
            List<IException> exceptions = new List<IException>();

            return exceptions;
        }
    }
}
