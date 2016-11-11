using System;
using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes.Decision;
using CommonTypes.Operation;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionOperationsChain : IExceptionSearch
    {
        private Dictionary<int, IOperation> operations;
        private List<IDecision> decisions;

        // Конструктор
        public ExceptionOperationsChain(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            //ConsoleLogger.Log("Ищем ошибки в условиях последовательного выполнения операций...");
            List<IException> exceptions = new List<IException>();

            DateTime maxEndFate;
            DateTime dateBegin;

            foreach (var decision in decisions)
            {
                dateBegin = decision.GetStartTime();
                maxEndFate = new DateTime(0);

                // Получаем список всех предшествующих операций текущей операции
                List<IOperation> prevOperations = decision.GetOperation().GetPrevOperations();

                // Находим для текущей операции максимальное время окончания всех её предшествующих операций
                foreach (var prevOperation in prevOperations)
                {
                    bool isFound = false;
                    // Находим каждую предыдущую операцию в построенном расписании
                    foreach (var completeDecision in decisions)
                    {
                        if (completeDecision.GetOperation() == prevOperation)
                        {
                            isFound = true;
                            // Если нашли, то проверяем её время окончания
                            if (completeDecision.GetEndTime() > maxEndFate)
                                maxEndFate = completeDecision.GetEndTime();
                        }
                    }
                    if(!isFound)
                    {
                        // Предыдущей операции нет в расписании
                        exceptions.Add(new Debugger.Exceptions.Exception("R00",
                                                 "Error",
                                                 "Было нарушено условие последовательного выполнения операций",
                                                 "Операции " + prevOperation.GetId() + " нет в расписании",
                                                 ""));
                        maxEndFate = new DateTime(0);
                        break;
                    }
                }

                // Сверяем время завершения
                if (dateBegin < maxEndFate)
                {
                    exceptions.Add(new Debugger.Exceptions.Exception("R00",
                                                 "Error",
                                                 "Было нарушено условие последовательного выполнения операций",
                                                 decision.GetOperation().ToString(),
                                                 ""));
                }
            }

            return exceptions;
        }
    }
}
