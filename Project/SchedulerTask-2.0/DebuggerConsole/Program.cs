using System;
using System.Collections.Generic;
using System.IO;
using Debugger;
using Debugger.IO;
using Debugger.Exceptions;
using Builder;
using Builder.Equipment;
using Debugger.FindExceptions;

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

            switch (comm_args.Length)
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
    /// Клиентсий код для отладчика
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //CommandLineParser argsParser = new CommandLineParser(args);

            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Пути к данным не указаны");
                Environment.Exit(1);
            }
            if (args.Length > 0 && !Directory.Exists(args[0]))
            {
                Console.WriteLine("Указанный путь к входным данным не существует");
                Environment.Exit(1);
            }
            if (args.Length > 1 && !Directory.Exists(args[1]))
            {
                Console.WriteLine("Указанный путь к выходным данным не существует");
                Environment.Exit(1);
            }

            Reader reader = null;
            try
            {
                reader = new Reader(args[0]);
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
                Environment.Exit(1);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Путь содержит недопустимые символы");
                Environment.Exit(1);
            }

            TestScheduleA testA = new TestScheduleA();
            ExceptionsSearch search = new ExceptionsSearch(testA.GetOperations(), testA.GetEquipment(), testA.GetDecisions(), null);


            //Dictionary<int, IOperation> operations;
            //Dictionary<int, IEquipment> equipments;
            //List<Decision> decisions;
            //reader.ReadData(out operations, out equipments, out decisions);
            //ExceptionsSearch search = new ExceptionsSearch()



            Writer writer = null;
            try
            {
                // Если передан один аргумент, то его используем как директорию для результирующих файлов
                writer = args.Length <= 1 ? new Writer(args[0]) : new Writer(args[1]);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Путь содержит недопустимые символы");
                Environment.Exit(1);
            }
            catch (FileNotFoundException) //игнорируем ошибку т.к. файл создается райтером
            {

            }

            List<IException> exceptions = search.Execute();
            ConsoleLogger.Log("Найдено ошибок : " + exceptions.Count);
            writer.WriteLog(exceptions);
        }
    }
}
