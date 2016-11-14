using System.Collections.Generic;
using CommonTypes.Decision;
using CommonTypes.Operation;
using CommonTypes;
using Debugger.Exceptions;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionInvalidStartDate : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<IDecision> decisions;

        public ExceptionInvalidStartDate(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            // Конструктор
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в сформированных начальных датах...");
            List<IException> exceptions = new List<IException>();

            System.DateTime startDate;
            foreach (var decision in decisions)
            {
                // Дата начала выполнения текущей операции в построенном расписании
                startDate = decision.GetStartTime();

                // TODO : Получить доступ к дате начала расписания
                if (decision.GetOperation().GetParty() != null)
                {
                    System.DateTime beginDate = decision.GetOperation().GetParty().GetStartTimeParty();
                    if (beginDate > startDate)
                    {
                        exceptions.Add(new Exception("V04",
                                                     "Error",
                                                     "Операция в построенном расписании не может быть начата раньше указанной даты в файле с исходными данными",
                                                     "",
                                                     "Номер операции : " + decision.GetOperation().GetId()));
                    }
                }

            }

            return exceptions;
        }
    }
}
