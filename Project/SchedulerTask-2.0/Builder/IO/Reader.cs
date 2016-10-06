using Builder.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.IO
{
    public class Reader
    {
        /// <summary>
        /// Список для хранения партий
        /// </summary>
        List<Party> partys;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, Operation> operations;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, IEquipment> equipments;
        
        

        /// <summary>
        /// констуктор ридера
        /// </summary>
        /// <param name="folderPatch"> Путь к .xml файлам.</param>
        public Reader(string folderPatch)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partys"> Список партий.</param>
        /// <param name="operations">Список операций.</param>
        /// <param name="equipments">Список оборудований.</param>
        public void ReadData(out List<Party> partys, out Dictionary<int, IOperation> operations, out Dictionary<int, IEquipment> equipments) 
        {
            partys = null;
            operations = null;
            equipments = null;
        }
    }
}
