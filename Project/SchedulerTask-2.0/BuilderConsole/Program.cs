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
            if (args == null)
            {
                Console.WriteLine("Входные данные не указаны");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            if (!System.IO.Directory.Exists(args[0]))
            {
                Console.WriteLine("Указанный путь к входным данным не существует");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            if (!System.IO.Directory.Exists(args[1]))
            {
                Console.WriteLine("Указанный путь к выходным данным не существует");
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


            List<Party> partys;
            Dictionary<int, IOperation> operations;
            Dictionary<int, IEquipment> equipments;
            reader.ReadData(out partys, out operations, out equipments);

            FrontBuilding frontBuilding = new FrontBuilding(partys);
            frontBuilding.Build();
            Writer writer = null;
            try
            {
                 writer = new Writer(args[0],args[1]);
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("Путь содержит недопустимые символы");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            catch (System.IO.FileNotFoundException) //игнорируем ошибку т.к. файл создается райтером
            {
                
            }
            writer.WriteData(partys);
        }
    }
}
