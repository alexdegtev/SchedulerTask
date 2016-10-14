using Builder;
using Builder.Equipment;
using Debagger.Exeptions;
using Debugger.Exeptions;
using Debugger.FindExeptions.Seachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debagger
{
    public class ExeptionsSearch
    {
        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, Operation> operations;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, IEquipment> equipments;

        List<Decision> decisions;

        List<IExeptionSearch> exeptionsSeachers;

        List<IExeption> exeptions;

        public ExeptionsSearch(Dictionary<int, Operation> operations, Dictionary<int, IEquipment> equipments, List<Decision> decisions)
        {

        }

        public List<IExeption> Execute()
        {
            throw new Exception();
        }
    }
}
