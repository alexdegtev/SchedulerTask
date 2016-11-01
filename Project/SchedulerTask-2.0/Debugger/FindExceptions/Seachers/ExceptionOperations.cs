using System.Collections.Generic;
using Debugger.Exceptions;
using Builder;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionOperations : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<Decision> decisions;

        public ExceptionOperations(Dictionary<int, IOperation> operations, List<Decision> decisions)
        {
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем несоответствия в начальных условиях и расписании...");
            List<IException> exceptions = new List<IException>();

            // количество совпадений
            int count = 0;
            foreach (var decision in decisions)
            {
                count = 0;
                foreach (var operation in operations)
                {
                    if (decision.GetOperation().GetId() == operation.Value.GetId())
                        count++;
                }
                if (count == 0)
                {
                    exceptions.Add(new Exception("V00",
                                                 "Error",
                                                 "Несоответствие набора исходных операций операциям в расписании : индентификатора операции " + decision.GetOperation().GetId() + " нет в исходных данных",
                                                 null,
                                                 null));
                }
            }

            return exceptions;
        }
    }
}
