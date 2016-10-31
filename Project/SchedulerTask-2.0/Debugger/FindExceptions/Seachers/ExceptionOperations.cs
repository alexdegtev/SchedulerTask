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
                    if (decision.GetOperation().GetID() == operation.Value.GetID())
                        count++;
                }
                if (count == 0)
                {
                    exceptions.Add(new Exception("V00",
                                                 "Error",
                                                 "Несоответствие набора исходных операций операциям в расписании : индентификатора операции " + decision.GetOperation().GetID() + " нет в исходных данных",
                                                 null,
                                                 null));
                }
            }

            return exceptions;
        }
    }
}