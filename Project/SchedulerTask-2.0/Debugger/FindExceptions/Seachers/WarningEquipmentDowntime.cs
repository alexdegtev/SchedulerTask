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
