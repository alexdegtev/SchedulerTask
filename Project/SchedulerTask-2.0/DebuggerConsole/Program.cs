using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Debugger;
using Debugger.IO;
using Debugger.Exceptions;
using Builder;
using Builder.Equipment;

namespace DebuggerConsole
{
    class TestScheduleA
    {
        Dictionary<int, Operation> operations;
        Dictionary<int, IEquipment> equipments;
        List<Decision> decisions;

        public TestScheduleA()
        {
            operations = new Dictionary<int, Operation>();
            equipments = new Dictionary<int, IEquipment>();
            decisions = new List<Decision>();

            // Список исходных операций
            operations.Add(1, new Operation(1, "Операция 1", new TimeSpan(200), new List<IOperation>(), new SingleEquipment(null, 1, "Станок 1"), null));
            operations.Add(2, new Operation(2, "Операция 2", new TimeSpan(100), new List<IOperation>(), new SingleEquipment(null, 1, "Станок 1"), null));
            operations.Add(3, new Operation(3, "Операция 3", new TimeSpan(200), new List<IOperation>(), new SingleEquipment(null, 2, "Станок 2"), null));
            operations.Add(4, new Operation(4, "Операция 4", new TimeSpan(200), new List<IOperation>(), new SingleEquipment(null, 3, "Станок 3"), null));

            // Список решений
            decisions.Add(new Decision(new DateTime(0), new DateTime(200), new SingleEquipment(null, 1, "Станок 1"), operations[1]));
            decisions.Add(new Decision(new DateTime(200), new DateTime(300), new SingleEquipment(null, 1, "Станок 1"), operations[2]));
            decisions.Add(new Decision(new DateTime(0), new DateTime(200), new SingleEquipment(null, 1, "Станок 2"), operations[3]));
            decisions.Add(new Decision(new DateTime(0), new DateTime(200), new SingleEquipment(null, 1, "Станок 3"), operations[4]));

            // недопустимая операция
            Operation invalidOperation = new Operation(5, "Недопустимая операция", new TimeSpan(), new List<IOperation>(), null, null);
            decisions.Add(new Decision(new DateTime(0), new DateTime(200), new SingleEquipment(null, 1, "Станок 3"), invalidOperation));
        }

        public Dictionary<int, Operation> GetOperations()
        {
            return operations;
        }

        public Dictionary<int, IEquipment> GetEquipment()
        {
            return equipments;
        }

        public List<Decision> GetDecisions()
        {
            return decisions;
        }
    }

    /// <summary>
    /// Клиентсий код для отладчика.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string pathToFolder = "";
            if (args == null)
            {
                Console.WriteLine("Пути к данным не указаны");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            Reader reader = null;
            try
            {
                reader = new Reader(args[0]);
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("По указанному пути файл не найден");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("Путь содержит недопустимые символы");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            TestScheduleA testA = new TestScheduleA();            

            ExceptionsSearch search = new ExceptionsSearch(testA.GetOperations(), testA.GetEquipment(), testA.GetDecisions(), null);

            Writer writer = new Writer(pathToFolder);
            List<IException> exceptions = search.Execute();
            writer.WriteLog(exceptions);
        }
    }
}
