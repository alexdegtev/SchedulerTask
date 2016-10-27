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
            Reader reader = new Reader(args[0]);

            List<Party> partys;
            Dictionary<int, IOperation> operations;
            Dictionary<int, IEquipment> equipments;
            reader.ReadData(out partys, out operations, out equipments);

            FrontBuilding frontBuilding = new FrontBuilding(partys);
            frontBuilding.Build();

            Writer writer = new Writer(args[1]);
            writer.WriteData(partys);
        }
    }
}
