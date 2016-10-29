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
    class WarningNotSheduled : IExceptionSearch
    {
        Dictionary<int, Operation> operations;
        List<Decision> decisions;

        public WarningNotSheduled(Dictionary<int, Operation> operations, List<Decision> decisions)
        {
            // Конструктор
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем операции, не вошедшие в расписание...");
            List<IException> exceptions = new List<IException>();
            bool found = false;

            // Проходим по всем данным операциям
            foreach(var operation in operations)
            {
                found = false;
                // Каждую текущую операцию ищем в назначенных операциях
                foreach(var decision in decisions)
                {
                    if (decision.GetOperation() == operation.Value)
                        found = true;
                }

                if(!found)
                {
                    exceptions.Add(new Exception("Q01",
                                                 "Warning",
                                                 "Операции номер " + operation.Value.GetID() + " нет в расписании",
                                                 null,
                                                 null));
                }
            }

            return exceptions;
        }
    }
}
