using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.IO;
using Builder.Equipment;
using Builder.Front;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader();

            Dictionary<int, IEquipment> eqdic;
            eqdic = reader.ReadSystemData();

            List<Party> partlist;
            Dictionary<int, IOperation> opdic;
            reader.ReadTechData(out partlist, out opdic);

            // eqdic.Values;

            EquipmentManager em = new EquipmentManager();

            FrontBuilding fb = new FrontBuilding(partlist);
            fb.Build();

            Writer w = new Writer();
            w.WriteData(opdic);
        }
    }
}
