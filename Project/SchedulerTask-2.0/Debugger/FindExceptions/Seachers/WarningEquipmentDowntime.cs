using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes;
using CommonTypes.Decision;
using CommonTypes.Operation;
using System;

namespace Debugger.FindExceptions.Seachers
{
    class WarningEquipmentDowntime : IExceptionSearch
    {
        Dictionary<int,IOperation> opdic;
        List<IDecision> dlist;

        // Конструктор
        public WarningEquipmentDowntime(List<IDecision> dlist, Dictionary<int,IOperation> opdic)
        {
            this.opdic = opdic;
            //doit
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем временные промежутки простоя оборудования...");
            List<IException> exceptions = new List<IException>();
            TimeSpan tmax = new TimeSpan(0);
            foreach (KeyValuePair<int,IOperation> operation in opdic)
            {
                
                if (operation.Value.GetDuration() > tmax) tmax = operation.Value.GetDuration();
            }

            foreach (var decision in dlist)
            {
                if (tmax < decision.GetEquipment().GetTimeWorkInTwentyFourHours())
                {
                    exceptions.Add(new Debugger.Exceptions.Exception("Q00",
                                                 "Warning",
                                                 "Возможны простои оборудования",
                                                 decision.ToString(),
                                                 ""));
                    break;
                }
            }


            return exceptions;
        }
    }
}
