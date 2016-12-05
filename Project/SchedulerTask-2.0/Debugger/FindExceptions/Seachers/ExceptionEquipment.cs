using System.Collections.Generic;
using Debugger.Exceptions;
using CommonTypes.Decision;
using CommonTypes.Operation;
using CommonTypes.Equipment;
using CommonTypes;

namespace Debugger.FindExceptions.Seachers
{
    class ExceptionEquipment : IExceptionSearch
    {
        Dictionary<int, IOperation> operations;
        List<IDecision> decisions;

        public ExceptionEquipment(Dictionary<int, IOperation> operations, List<IDecision> decisions)
        {
            // Конструктор
            this.operations = operations;
            this.decisions = decisions;
        }

        public List<IException> Execute()
        {
            ConsoleLogger.Log("Ищем ошибки в соответствии оборудования операциям...");
            List<IException> exceptions = new List<IException>();

            foreach(var decision in decisions)
            {
                int singleEquipmentId = decision.GetEquipment().GetId();
                int groupEquipmentId  = decision.GetEquipment().GetParentGroupId();
                
                // Ищем соответветствующую операцию в исходных данных
                foreach(var operation in operations)
                {
                    // Нашли соответветствующую операцию в исходном расписании
                    if(decision.GetOperation() == operation.Value)
                    {
                        // Находим номер оборудования или группы оборудования, на котором должна выполняться операция
                        int operationEquipmentId = operation.Value.GetEquipment().GetId();
                        bool isCorrect = false;

                        if (singleEquipmentId == operationEquipmentId || groupEquipmentId == operationEquipmentId)
                            isCorrect = true;
                        else
                            isCorrect = false;

                        if(!isCorrect)
                        {
                            exceptions.Add(new Debugger.Exceptions.Exception("V01",
                                                 "Error",
                                                 "Несоответствие оборудования и выполняемой на нем операции",
                                                 "",
                                                 ""));
                        }
                    }
                }
            }

            return exceptions;
        }
    }
}
