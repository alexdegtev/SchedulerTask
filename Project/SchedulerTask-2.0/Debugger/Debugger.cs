using Builder;
using Builder.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debagger
{
    /// <summary>
    /// Facade for Debugger.
    /// </summary>
    public class Debugger
    {
        private string inputDir;
        private string outputDir;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, Operation> operations;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, IEquipment> equipments;

        public Debugger(string inputDir, string outputDir)
        {
            this.inputDir = inputDir;
            this.outputDir = outputDir;
        }

        public void Run()
        {

        }
    }
}