using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes.Decision;
using CommonTypes.Operation;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionSimultaneityCondition : IExceptionSearch
    {
        private Dictionary<int, IOperation> operations;
        private List<IDecision> decisions;

        // Конструктор
        public ExceptionSimultaneityCondition(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            List<IException> exceptions = new List<IException>();

            foreach (var first in decisions)
            {
                foreach (var second in decisions)
                {
                    if (first == second) continue;
                    if (first.GetEquipment().GetId() == second.GetEquipment().GetId())
                    {
                        if(first.GetStartTime() < second.GetEndTime() && first.GetEndTime() > second.GetStartTime())
                            exceptions.Add(new Exception("R01",
                                                "Error",
                                                "Запрещено одновременное использование ресурса несколькими операциями",
                                                first.ToString(),
                                                second.ToString()));
                    }
                }
            }

            return exceptions;
        }
    }
}
