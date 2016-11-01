using System.Collections.Generic;
using Builder;
using Builder.Equipment;
using Debugger.Exceptions;
using Debugger.FindExceptions.Seachers;

namespace Debugger.FindExceptions
{
    public class ExceptionsSearch
    {
        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, IOperation> operations;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, IEquipment> equipments;

        List<Decision> decisions;

        List<IExceptionSearch> exceptionsSeachers;

        List<IException> exceptions;

        List<Party> partys;

        public ExceptionsSearch(Dictionary<int, IOperation> operations, Dictionary<int, IEquipment> equipments, List<Decision> decisions, List<Party> partys)
        {
            this.operations = operations;
            this.equipments = equipments;
            this.decisions = decisions;
            this.partys = partys;

            // Создание списка объектов поиска ошибок
            exceptionsSeachers = new List<IExceptionSearch>();
            //exceptionsSeachers.Add(new MockupException());
            exceptionsSeachers.Add(new ExceptionOperations(operations, decisions));
            exceptionsSeachers.Add(new ExceptionEquipment());
            exceptionsSeachers.Add(new ExceptionOperationDuration());
            //exceptionsSeachers.Add(new ExceptionInvalidStartDate(operations, decisions));
            exceptionsSeachers.Add(new ExceptionOperationsChain(operations, decisions));
            //exceptionsSeachers.Add(new ExceptionSimultaneityCondition(operations, decisions));
            exceptionsSeachers.Add(new ExceptionTimePeriod(decisions));
            exceptionsSeachers.Add(new ExceptionInvalidDate());
            exceptionsSeachers.Add(new WarningTimePeriod());
            exceptionsSeachers.Add(new WarningEquipmentDowntime());
            exceptionsSeachers.Add(new WarningNotSheduled(operations, decisions));
        }

        public List<IException> Execute()
        {
            // Находим ошибки и добавляем их в список
            exceptions = new List<IException>();
            foreach (var search in exceptionsSeachers)
                exceptions.AddRange(search.Execute());

            return exceptions;
        }
    }
}
