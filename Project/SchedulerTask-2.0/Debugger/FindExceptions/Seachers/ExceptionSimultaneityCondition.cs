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

            List<KeyValuePair<Decision, Decision>> similarOperations = new List<KeyValuePair<Decision, Decision>>();
            // Сравниваем все операции в расписании друг с другом
            foreach (var decision in decisions)
            {
                foreach (var otherDecision in decisions)
                {
                    if (decision != otherDecision
                        && decision.GetOperation().GetEquipment().GetID()
                        == otherDecision.GetOperation().GetEquipment().GetID())
                    {
                        if(   !similarOperations.Contains(new KeyValuePair<Decision, Decision>(decision, otherDecision))
                           && !similarOperations.Contains(new KeyValuePair<Decision, Decision>(otherDecision, decision)))
                        {
                            similarOperations.Add(new KeyValuePair<Decision, Decision>(decision, otherDecision));
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
                                                 pairOperations.Key.ToString(),
                                                 pairOperations.Value.ToString()));
                }
            }

            return exceptions;
        }
    }
}
