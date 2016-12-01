using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes.Decision;
using CommonTypes.Operation;
using CommonTypes;

namespace Debugger.FindExceptions.Seachers
{
    class WarningNotSheduled : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<IDecision> decisions;

        // Конструктор
        public WarningNotSheduled(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Проверяем наличие всех операций в расписании");
            List<IException> exceptions = new List<IException>();

            foreach (var decision in decisions)
            {
                if (!decision.IsSchduled())
                {
                    exceptions.Add(new Exception("R04",
                                                 "Error",
                                                 "Не все операции были назначены",
                                                 decision.ToString(),
                                                 ""));
                }
            }


            return exceptions;
        }
    }
}
