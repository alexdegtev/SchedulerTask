using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.Equipment;
using Builder.Front;
using Builder.Front.Building;
using Builder.IO;

namespace Builder
{
    /// <summary>
    /// Facade for Builder.
    /// </summary>
    public class BuilderScheduler
    {
        private string inputDir;
        private string outputDir;

        public BuilderScheduler(string inputDir, string outputDir)
        {
            this.inputDir = inputDir;
            this.outputDir = outputDir;
        }

        public void Run()
        {
            Reader reader = new Reader(inputDir);

            List<Party> partys;
            Dictionary<int, IOperation> operations;
            Dictionary<int, IEquipment> equipments;
            reader.ReadData(out partys, out operations, out equipments);

            FrontBuilding frontBuilding = new FrontBuilding(partys);
            frontBuilding.Build();            

            Writer writer = new Writer(outputDir);
            writer.WriteData(partys);
        }
    }
}
