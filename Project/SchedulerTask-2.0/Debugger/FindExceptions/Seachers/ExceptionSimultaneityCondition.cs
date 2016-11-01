﻿using System.Collections.Generic;
using Debugger.Exceptions;
using Builder;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionSimultaneityCondition : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<Decision> decisions;

        public ExceptionSimultaneityCondition(Dictionary<int, IOperation> operations, List<Decision> decisions)
        {
            // Конструктор
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Проверяем условие одновременного использования оборудования...");
            List<IException> exceptions = new List<IException>();

            Dictionary<Decision, Decision> similarOperations = new Dictionary<Decision, Decision>();
            // Сравниваем все операции в расписании друг с другом
            foreach (var decision in decisions)
            {
                foreach (var otherDecision in decisions)
                {
                    if (decision != otherDecision
                        && decision.GetOperation().GetEquipment().GetID()
                        == otherDecision.GetOperation().GetEquipment().GetID())
                    {
                        if (!similarOperations.ContainsKey(decision)
                            || !similarOperations.ContainsValue(otherDecision))
                        {
                            similarOperations.Add(decision, otherDecision);
                        }
                    }
                }
            }

            // В списке операций с одним и тем же оборудованием, проверяем, чтобы отрезки времени их выполнения не пересекались
            foreach (var pairOperations in similarOperations)
            {
                if (pairOperations.Key.GetStartTime() < pairOperations.Value.GetStartTime()
                    && pairOperations.Key.GetEndTime() > pairOperations.Value.GetStartTime()
                    || pairOperations.Value.GetStartTime() < pairOperations.Key.GetStartTime()
                    && pairOperations.Value.GetEndTime() > pairOperations.Key.GetStartTime())
                {
                    exceptions.Add(new Exception("R01",
                                                 "Error",
                                                 "Запрещено одновременное использование ресурса несколькими операциями",
                                                 "Операция " + pairOperations.Key.GetOperation().GetId(),
                                                 "Операция " + pairOperations.Value.GetOperation().GetId()));
                }
            }

            return exceptions;
        }
    }
}
