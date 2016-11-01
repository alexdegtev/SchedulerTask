using Builder;
using Builder.Equipment;
using Builder.Front;
using Builder.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace BuilderConsole
{
    /// <summary>
    /// Клиентский код для построителя
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
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

            List<Party> partys;
            Dictionary<int, IOperation> operations;
            Dictionary<int, IEquipment> equipments;
            reader.ReadData(out partys, out operations, out equipments);

            FrontBuilding frontBuilding = new FrontBuilding(partys);
            frontBuilding.Build();

            Writer writer = null;
            try
            {
                // Если передан один аргумент, то его используем как директорию для результирующих файлов
                writer = args.Length <= 1 ? new Writer(args[0], args[0]) : new Writer(args[0], args[1]);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Путь содержит недопустимые символы");
                Environment.Exit(1);
            }
            catch (FileNotFoundException) //игнорируем ошибку т.к. файл создается райтером
            {

            }
            writer.WriteData(partys);
        }
    }
}
