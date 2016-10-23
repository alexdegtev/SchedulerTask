using Builder;
using Builder.Equipment;
using Debugger.Exceptions;
using Debugger.FindExceptions.Seachers;
//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger
{
    public class ExceptionsSearch
    {
        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, Operation> operations;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, IEquipment> equipments;

        List<Decision> decisions;

        List<IExceptionSearch> exceptionsSeachers;

        List<IException> exceptions;

        public ExceptionsSearch(Dictionary<int, Operation> operations, Dictionary<int, IEquipment> equipments, List<Decision> decisions)
        {
            this.operations = operations;
            this.equipments = equipments;
            this.decisions = decisions;

            // Создание списка объектов поиска ошибок
            exceptionsSeachers = new List<IExceptionSearch>();
            //exceptionsSeachers.Add(new MockupException());
            exceptionsSeachers.Add(new ExceptionOperations(operations, decisions));

            // Находим ошибки и добавляем их в список
            exceptions = new List<IException>();
            foreach (var search in exceptionsSeachers)
                exceptions.AddRange(search.Execute());
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
