using Builder;
using Builder.Equipment;
using Builder.Front;
using Builder.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuilderConsole
{
    /// <summary>
    /// Клиентский код для построителя.
    /// </summary>
    class Program
    {
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

            List<Party> partys;
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
