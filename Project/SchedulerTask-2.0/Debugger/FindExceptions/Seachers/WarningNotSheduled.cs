using System.Collections.Generic;
using Debugger.Exceptions;
using Builder;

namespace Debugger.FindExceptions.Seachers
{
    class WarningNotSheduled : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<Decision> decisions;

        public WarningNotSheduled(Dictionary<int, IOperation> operations, List<Decision> decisions)
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
            foreach (var operation in operations)
            {
                found = false;
                // Каждую текущую операцию ищем в назначенных операциях
                foreach (var decision in decisions)
                {
                    if (decision.GetOperation() == operation.Value)
                        found = true;
                }

                if (!found)
                {
                    exceptions.Add(new Exception("Q01",
                                                 "Warning",
                                                 "Операции номер " + operation.Value.GetId() + " нет в расписании",
                                                 null,
                                                 null));
                }
            }

            return exceptions;
        }
    }
}
