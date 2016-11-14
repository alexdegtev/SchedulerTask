using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes.Decision;
using CommonTypes.Operation;
using CommonTypes;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionPreviousOperations : IExceptionSearch
    {
        private Dictionary<int, IOperation> operations;
        private List<IDecision> decisions;

        public ExceptionPreviousOperations(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в предшествующих операциях...");
            List<IException> exceptions = new List<IException>();

            foreach(var decision in decisions)
            {
                if(decision.GetOperation().GetPrevOperations() != null)
                {
                    List<IOperation> prevOperations = decision.GetOperation().GetPrevOperations();
                    bool isFound;
                    // Ищем в расписании все операции, предшествующей данной
                    foreach(var prevOperation in prevOperations)
                    {
                        isFound = false;
                        
                        // Ищем текущую предшествующую операцию в расписании
                        foreach(var sheduledOperation in decisions)
                        {
                            if(sheduledOperation.GetOperation().GetId() == prevOperation.GetId())
                            {
                                isFound = true;
                                break;
                            }
                        }

                        if(!isFound)
                        {
                            exceptions.Add(new Exception("V03",
                                                 "Error",
                                                 "Несоответствие набора исходных операций операциям в расписании",
                                                 prevOperation.ToString(),
                                                 ""));
                        }
                    }
                }
            }

            return exceptions;
        }
    }
}
