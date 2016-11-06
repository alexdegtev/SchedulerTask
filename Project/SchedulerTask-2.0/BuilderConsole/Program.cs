using Builder.Front;
using Builder.IO;
using System;
using System.Collections.Generic;
using CommonTypes;
using CommonTypes.Equipment;
using CommonTypes.Operation;
using CommonTypes.Party;

namespace BuilderConsole
{
    /// <summary>
    /// Клиентский код для генератора расписания
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineParser argsParser = new CommandLineParser(args);
            if (!argsParser.IsCorrect())
            {
                Environment.Exit(1);
            }

            try
            {
                Reader.SetFolderPath(argsParser.GetInputDir());
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                System.Environment.Exit(1);
            }

            List<IParty> partys;
            Dictionary<int, IOperation> operations;
            Dictionary<int, IEquipment> equipments;
            Reader.ReadData(out partys, out operations, out equipments);

            FrontBuilding frontBuilding = new FrontBuilding(partys);
            frontBuilding.Build();

            Writer writer = null;
            try
            {
                writer = new Writer(argsParser.GetInputDir(), argsParser.GetOutputDir());
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("Путь содержит недопустимые символы");
                System.Environment.Exit(1);
            }
            catch (System.IO.FileNotFoundException) //игнорируем ошибку т.к. файл создается райтером
            {
            }
            writer.WriteData(partys);
        }
    }
}
