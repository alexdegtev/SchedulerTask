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
            foreach(var operation in operations)
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

    public class CommandLineParser
    {
        private string[] comm_args;
        private bool is_correct;
        private string input_dir;
        private string output_dir;

        public CommandLineParser(string[] args)
        {
            comm_args = args;
            input_dir = "";
            output_dir = "";

            switch(comm_args.Length)
            {
                case 0:
                        comm_args = Environment.GetCommandLineArgs();
                        if (comm_args.Length != 0)
                        {
                            int position = comm_args[0].LastIndexOf("\\");
                            if (position == -1)
                            {
                                comm_args[0] = Directory.GetCurrentDirectory();
                                position = comm_args[0].LastIndexOf("\\");
                            }

                            for (int i = 0; i <= position; i++)
                            {
                                input_dir += comm_args[0][i];
                                output_dir += comm_args[0][i];
                            }
                            is_correct = true;
                        }
                        else
                        {
                            Console.WriteLine("Не удалось получить параметры");
                            is_correct = false;
                        }
                        break;
                case 1:
                        if (Directory.Exists(comm_args[0]))
                        {
                            input_dir = comm_args[0];
                            output_dir = Directory.GetCurrentDirectory();
                            is_correct = true;
                        }
                        else
                        {
                            is_correct = false;
                        }
                        break;
                case 2:
                        if (Directory.Exists(comm_args[0]) && Directory.Exists(comm_args[1]))
                        {
                            input_dir = comm_args[0];
                            output_dir = comm_args[1];
                            is_correct = true;
                        }
                        else
                        {
                            is_correct = false;
                        }
                        break;
            }
        }

        public string GetInputDir()
        {
            return input_dir;
        }

        public string GetOutputDir()
        {
            return output_dir;
        }

        public bool IsCorrect()
        {
            return is_correct;
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
        }

        static void Main(string[] args)
        {
            CommandLineParser argsParser = new CommandLineParser(args);
            if (!argsParser.IsCorrect())
            {  
                Console.WriteLine("Неверная входная командная строка");
                Environment.Exit(1);
            }
            Console.WriteLine("Директория с входными файлами : " + argsParser.GetInputDir());
            Console.WriteLine("Директория для записи лога    : " + argsParser.GetOutputDir());

            ExceptionsSearch search                 = null;
            List<IException> exceptions             = null;
            List<Decision> decisions                = null;
            List<Party> parties                     = null;
            Dictionary<int, IOperation> operations  = null;
            Dictionary<int, IEquipment> equipment   = null;
            Reader reader                           = null;
            Builder.IO.Reader builder_reader        = null;
            Writer writer                           = null;
            if (Options.is_debug)
            {
                TestScheduleA testA = new TestScheduleA();
                search = new ExceptionsSearch(testA.GetOperations(), testA.GetEquipment(), testA.GetDecisions(), null);
                exceptions = search.Execute();
                ConsoleLogger.Log("Найдено ошибок : " + exceptions.Count);
            }

            try
            {
                reader          = new Reader(argsParser.GetInputDir());
                builder_reader  = new Builder.IO.Reader(argsParser.GetInputDir());
                writer          = new Writer(argsParser.GetOutputDir());
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

            reader.ReadData(out decisions);
            builder_reader.ReadData(out parties, out operations, out equipment);
            if(decisions.Count == 0)
            {
                Console.WriteLine("Предупреждение : Построенное расписание не содержит операций");
            }
            if(parties.Count == 0)
            {
                Console.WriteLine("Предупреждение : Исходное расписание не содержит партий");
            }
            if(operations.Count == 0)
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
