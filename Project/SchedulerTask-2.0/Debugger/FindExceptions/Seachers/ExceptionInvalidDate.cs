using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes.Decision;
using CommonTypes.Operation;
using CommonTypes.Equipment;
using CommonTypes;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionInvalidDate : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<IDecision> decisions;

        public ExceptionInvalidDate(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            // Конструктор
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в назначении времени выполнения операций...");
            List<IException> exceptions = new List<IException>();

            foreach(var decision in decisions)
            {
                System.DateTime start = decision.GetStartTime();
                System.DateTime finish = decision.GetEndTime();
                bool isWorkTime = false;

                decision.GetEquipment().GetCalendar().FindInterval(start, out isWorkTime);
                if(!isWorkTime)
                {
                    exceptions.Add(new Exception("R03",
                                                 "Error",
                                                 "Начало выполнения операции в недоступное время для оборудования",
                                                 "Номер оборудования : " + decision.GetEquipment().GetId(),
                                                 "Номер операции : " + decision.GetOperation().GetId()));
                }

                decision.GetEquipment().GetCalendar().FindInterval(finish, out isWorkTime);
                if (!isWorkTime)
                {
                    exceptions.Add(new Exception("R03",
                                                 "Error",
                                                 "Завершение операции в недоступное время для оборудования ",
                                                 "Номер оборудования : " + decision.GetEquipment().GetId(),
                                                 "Номер операции : " + decision.GetOperation().GetId()));
                }

            }

            return exceptions;
        }
    }
}
