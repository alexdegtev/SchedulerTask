using System;
using System.IO;
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
            operations.Add(5, new Operation(5, "Операция 5", new TimeSpan(100), new List<IOperation>(), new SingleEquipment(null, 2, "Станок 2"), null));
            operations.Add(4, new Operation(4, "Операция 4", new TimeSpan(200), new List<IOperation>(), new SingleEquipment(null, 3, "Станок 3"), null));
            operations.Add(6, new Operation(6, "Операция 6", new TimeSpan(100), new List<IOperation>(), new SingleEquipment(null, 3, "Станок 3"), null));

            // Список решений
            decisions.Add(new Decision(new DateTime(0), new DateTime(200), new SingleEquipment(null, 1, "Станок 1"), operations[1]));
            decisions.Add(new Decision(new DateTime(200), new DateTime(300), new SingleEquipment(null, 1, "Станок 1"), operations[2]));
            decisions.Add(new Decision(new DateTime(0), new DateTime(200), new SingleEquipment(null, 1, "Станок 2"), operations[3]));
            decisions.Add(new Decision(new DateTime(0), new DateTime(200), new SingleEquipment(null, 1, "Станок 3"), operations[4]));

            // недопустимая операция
            Operation invalidOperation = new Operation(7, "Недопустимая операция", new TimeSpan(), new List<IOperation>(), null, null);
            decisions.Add(new Decision(new DateTime(0), new DateTime(200), new SingleEquipment(null, 1, "Станок 3"), invalidOperation));
        }

        public Dictionary<int, IOperation> GetOperations()
        {
            Dictionary<int, IOperation> out_operations = new Dictionary<int, IOperation>();
            foreach (var operation in operations)
            {
                out_operations.Add(operation.Key, operation.Value);
            }
            return out_operations;
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

    class TestScheduleB
    {
        Dictionary<int, Operation> operations;
        Dictionary<int, IEquipment> equipments;
        List<Decision> decisions;

        public TestScheduleB()
        {
            operations = new Dictionary<int, Operation>();
            equipments = new Dictionary<int, IEquipment>();
            decisions = new List<Decision>();

            // Список исходных операций
            //  - id операции
            //  - Название операции
            //  - Продолжительность операции
            //  - список предыдущих операций
            //  - оборудование -> Календарь, Id, название
            //  - партия
            operations.Add(1, new Operation(1, "Операция 1", new TimeSpan(300), new List<IOperation>(), new SingleEquipment(null, 1, "Станок 1"), null));
            operations.Add(2, new Operation(2, "Операция 2", new TimeSpan(300), new List<IOperation>(), new SingleEquipment(null, 2, "Станок 2"), null));
            operations.Add(3, new Operation(3, "Операция 3", new TimeSpan(300), new List<IOperation>(), new SingleEquipment(null, 2, "Станок 2"), null));
            operations.Add(4, new Operation(4, "Недопустимая операция 4",   new TimeSpan(200), new List<IOperation>(), new SingleEquipment(null, 3, "Станок 3"), null));
            operations.Add(5, new Operation(5, "Неиспользуемая операция 5", new TimeSpan(200), new List<IOperation>(), new SingleEquipment(null, 3, "Станок 3"), null));

            // Список решений
            decisions.Add(new Decision(new DateTime(0),   new DateTime(300), new SingleEquipment(null, 1, "Станок 1"), operations[1]));
            decisions.Add(new Decision(new DateTime(0),   new DateTime(300), new SingleEquipment(null, 2, "Станок 2"), operations[2]));
            decisions.Add(new Decision(new DateTime(100), new DateTime(400), new SingleEquipment(null, 2, "Станок 2"), operations[3]));
            decisions.Add(new Decision(new DateTime(300), new DateTime(100), new SingleEquipment(null, 3, "Станок 3"), operations[4]));
        }

        public Dictionary<int, IOperation> GetOperations()
        {
            Dictionary<int, IOperation> out_operations = new Dictionary<int, IOperation>();
            foreach (var operation in operations)
            {
                out_operations.Add(operation.Key, operation.Value);
            }
            return out_operations;
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

    class TestScheduleC
    {
        Dictionary<int, Operation> operations;
        Dictionary<int, IEquipment> equipments;
        List<Decision> decisions;

        public TestScheduleC()
        {
            operations = new Dictionary<int, Operation>();
            equipments = new Dictionary<int, IEquipment>();
            decisions = new List<Decision>();

            Operation op1 = new Operation(1, "Операция 1", new TimeSpan(300), new List<IOperation>(), new SingleEquipment(null, 1, "Станок 1"), null);
            Operation op2 = new Operation(2, "Операция 2", new TimeSpan(300), new List<IOperation>(), new SingleEquipment(null, 1, "Станок 1"), null);
            Operation op3 = new Operation(3, "Операция 3", new TimeSpan(200), new List<IOperation>(), new SingleEquipment(null, 1, "Станок 1"), null);

            // Список предыдущих операций
            List<IOperation> prev_operations = new List<IOperation>();
            prev_operations.Add(op1);
            prev_operations.Add(op2);
            prev_operations.Add(op3);
            Operation op4 = new Operation(4, "Операция 4", new TimeSpan(200), prev_operations, new SingleEquipment(null, 1, "Станок 1"), null);

            SingleEquipment equipment = new SingleEquipment(null, 1, "Станок 1");

            // Список исходных операций
            operations.Add(1, op1);
            operations.Add(2, op2);
            operations.Add(3, op3);
            operations.Add(4, op4);

            // Список решений
            decisions.Add(new Decision(new DateTime(0), new DateTime(300), equipment, operations[1]));
            decisions.Add(new Decision(new DateTime(300), new DateTime(500), equipment, operations[3]));
            decisions.Add(new Decision(new DateTime(500), new DateTime(700), equipment, operations[4]));
        }

        public Dictionary<int, IOperation> GetOperations()
        {
            Dictionary<int, IOperation> out_operations = new Dictionary<int, IOperation>();
            foreach (var operation in operations)
            {
                out_operations.Add(operation.Key, operation.Value);
            }
            return out_operations;
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
        public struct Options
        {
            static public bool is_debug = false;
            static public int test_id = 0;
        }

        static void Main(string[] args)
        {
            Builder.IO.CommandLineParser argsParser = new Builder.IO.CommandLineParser(args);
            if (!argsParser.IsCorrect())
            {
                Console.WriteLine("Неверная входная командная строка");
                Environment.Exit(1);
            }
            Console.WriteLine("Директория с входными файлами : " + argsParser.GetInputDir());
            Console.WriteLine("Директория для записи лога    : " + argsParser.GetOutputDir());

            ExceptionsSearch search = null;
            List<IException> exceptions = null;
            List<Decision> decisions = null;
            List<Party> parties = null;
            Dictionary<int, IOperation> operations = null;
            Dictionary<int, IEquipment> equipment = null;
            Reader reader = null;
            Builder.IO.Reader builder_reader = null;
            Writer writer = null;

            // Тестовые расписания
            if (Options.is_debug)
            {
                switch(Options.test_id)
                {
                    case 0:
                        TestScheduleA testA = new TestScheduleA();
                        search = new ExceptionsSearch(testA.GetOperations(), testA.GetEquipment(), testA.GetDecisions(), null);
                        break;
                    case 1:
                        TestScheduleB testB = new TestScheduleB();
                        search = new ExceptionsSearch(testB.GetOperations(), testB.GetEquipment(), testB.GetDecisions(), null);
                        break;
                    case 2:
                        TestScheduleC testC = new TestScheduleC();
                        search = new ExceptionsSearch(testC.GetOperations(), testC.GetEquipment(), testC.GetDecisions(), null);
                        break;
                }

                exceptions = search.Execute();
                ConsoleLogger.Log("Найдено ошибок : " + exceptions.Count);
            }

            try
            {
                reader = new Reader(argsParser.GetInputDir());
                builder_reader = new Builder.IO.Reader(argsParser.GetInputDir());
                writer = new Writer(argsParser.GetOutputDir());
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("По указанному пути файл не найден");
                System.Environment.Exit(1);
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("Путь содержит недопустимые символы");
                System.Environment.Exit(1);
            }

            reader.ReadData(out decisions);
            builder_reader.ReadData(out parties, out operations, out equipment);
            if (decisions.Count == 0)
            {
                Console.WriteLine("Предупреждение : Построенное расписание не содержит операций");
            }
            if (parties.Count == 0)
            {
                Console.WriteLine("Предупреждение : Исходное расписание не содержит партий");
            }
            if (operations.Count == 0)
            {
                Console.WriteLine("Предупреждение : Исходное расписание не содержит операций");
            }
            if (equipment.Count == 0)
            {
                Console.WriteLine("Предупреждение : Построенное расписание не содержит оборудования");
            }


            search = new ExceptionsSearch(operations, equipment, decisions, parties);
            search.Execute();
            ConsoleLogger.Log("Найдено ошибок : " + exceptions.Count);
            writer.WriteLog(exceptions);
        }
    }
}
