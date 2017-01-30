using System;
using System.Collections.Generic;
using Debugger.IO;
using Debugger.Exceptions;
using CommonTypes;
using CommonTypes.Decision;
using CommonTypes.Equipment;
using CommonTypes.Operation;
using CommonTypes.Party;
using Debugger.FindExceptions;

namespace DebuggerConsole
{
    /// <summary>
    /// Клиентсий код для отладчика
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineParser argsParser = new CommandLineParser(args);
            if (!argsParser.IsCorrect())
            {
                Console.WriteLine("Неверная входная командная строка");
                Environment.Exit(1);
            }
            //if(!argsParser.isInfolog())
            //{
            //    ConsoleLogger.Enable();
            //}

            List<IParty> parties = new List<IParty>();
            Dictionary<int, IOperation> operations = new Dictionary<int, IOperation>();
            Dictionary<int, IEquipment> equipment = new Dictionary<int, IEquipment>();
            List<IDecision> decisions = null;

            try
            {
                Builder.IO.Reader.SetFolderPath(argsParser.GetInputDir());
                Builder.IO.Reader.ReadData(out parties, out operations, out equipment);

                Reader reader = new Reader(argsParser.GetInputDir(), argsParser.GetOutputDir());
                reader.ReadData(out decisions);
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
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                System.Environment.Exit(1);
            }


            if (decisions.Count == 0)
            {
                Console.WriteLine("Предупреждение: Построенное расписание не содержит операций");
            }
            if (parties.Count == 0)
            {
                Console.WriteLine("Предупреждение: Исходное расписание не содержит партий");
            }
            if (operations.Count == 0)
            {
                Console.WriteLine("Предупреждение: Исходное расписание не содержит операций");
            }
            if (equipment.Count == 0)
            {
                Console.WriteLine("Предупреждение: Построенное расписание не содержит оборудования");
            }


            ExceptionsSearch search = new ExceptionsSearch(operations, equipment, decisions, parties);
            List<IException> exceptions = search.Execute();

            Writer writer = new Writer(argsParser.GetOutputDir());
            writer.WriteLog(exceptions);
        }
    }
}
