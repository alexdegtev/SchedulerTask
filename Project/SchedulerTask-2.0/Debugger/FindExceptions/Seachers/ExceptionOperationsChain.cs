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

            DateTime max_end_date;
            DateTime date_begin;

            List<IOperation> prev_operations = new List<IOperation>();
            foreach (var decision in decisions)
            {
                date_begin = decision.GetStartTime();
                max_end_date = new DateTime(0);

                // Получаем список всех предшествующих операций текущей операции
                prev_operations = decision.GetOperation().GetPrevOperations();

                // Находим для текущей операции максимальное время окончания всех её предшествующих операций
                bool is_found;
                foreach (var prev_operation in prev_operations)
                {
                    is_found = false;
                    // Находим каждую предыдущую операцию в построенном расписании
                    foreach (var complete_decision in decisions)
                    {
                        if (complete_decision.GetOperation() == prev_operation)
                        {
                            is_found = true;
                            // Если нашли, то проверяем её время окончания
                            if (complete_decision.GetEndTime() > max_end_date)
                                max_end_date = decision.GetEndTime();
                        }
                    }
                    if(!is_found)
                    {
                        // Предыдущей операции нет в расписании
                        exceptions.Add(new Debugger.Exceptions.Exception("R00",
                                                 "Error",
                                                 "Было нарушено условие последовательного выполнения операций : операции " + prev_operation.GetID() + " нет в расписании",
                                                 "",
                                                 ""));
                        max_end_date = new DateTime(0);
                        break;
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
