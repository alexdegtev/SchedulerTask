using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Debugger;
using Debugger.Exceptions;
using Builder;
using Builder.Equipment;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionOperationsChain : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<Decision> decisions;

        public ExceptionOperationsChain(Dictionary<int, IOperation> operations, List<Decision> decisions)
        {
            // Конструктор
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в условиях последовательного выполнения операций...");
            List<IException> exceptions = new List<IException>();

            DateTime max_end_date = new DateTime(0);
            DateTime date_begin = new DateTime(0);
            List<IOperation> prev_operations = new List<IOperation>();
            foreach (var operation in operations)
            {
                // Получаем список всех предшествующих операций текущей операции
                prev_operations = operation.Value.GetPrevOperations();

                // Находим для текущей операции максимальное время окончания всех её предшествующих операций
                foreach (var prev_operation in prev_operations)
                {
                    // Находим каждую предыдущую операцию в построенном расписании
                    foreach (var decision in decisions)
                    {
                        if (decision.GetOperation() == prev_operation)
                        {
                            // Если нашли, то проверяем её время окончания
                            if (decision.GetEndTime() > max_end_date)
                                max_end_date = decision.GetEndTime();
                        }
                    }
                }

                // Находим текущую операцию в построенном расписании и находим её время начала
                foreach (var decision in decisions)
                {
                    if (decision.GetOperation() == operation.Value)
                    {
                        // Если нашли, то узнаём время начала операции
                        date_begin = decision.GetStartTime();
                    }
                }

                // Сверяем время завершения
                if (date_begin < max_end_date)
                {
                    exceptions.Add(new Debugger.Exceptions.Exception("R00",
                                                 "Error",
                                                 "Было нарушено условие последовательного выполнения операций",
                                                 null,
                                                 null));
                }
            }

            return exceptions;
        }
    }
}
