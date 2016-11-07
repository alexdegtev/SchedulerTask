using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes.Decision;
using CommonTypes.Operation;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionSimultaneityCondition : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<IDecision> decisions;

        public ExceptionSimultaneityCondition(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            // Конструктор
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Проверяем условие одновременного использования оборудования...");
            List<IException> exceptions = new List<IException>();

            List<KeyValuePair<IDecision, IDecision>> similarOperations = new List<KeyValuePair<IDecision, IDecision>>();
            // Сравниваем все операции в расписании друг с другом
            foreach (var decision in decisions)
            {
                foreach (var otherDecision in decisions)
                {
                    if (decision != otherDecision
                        && decision.GetOperation().GetEquipment().GetId()
                        == otherDecision.GetOperation().GetEquipment().GetId())
                    {
                        if(   !similarOperations.Contains(new KeyValuePair<IDecision, IDecision>(decision, otherDecision))
                           && !similarOperations.Contains(new KeyValuePair<IDecision, IDecision>(otherDecision, decision)))
                        {
                            similarOperations.Add(new KeyValuePair<IDecision, IDecision>(decision, otherDecision));
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
