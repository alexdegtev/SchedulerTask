using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Equipment
{
    class EquipmentManager
    {

        /// <summary>
        /// Поиск свободного оборудования в списке; (возвращаем true, если находим свободное оборудование, false - иначе);
        /// Доп. выходные параметры:
        /// operationtime - время окончания операции (для первого случая) или  ближайшее время начала операции (для второго случая); 
        /// </summary>       
        
        public static bool IsFree(DateTime T, IOperation o, out DateTime operationtime, out SingleEquipment equip)
        {
            operationtime = DateTime.MaxValue;
            equip = null;
            return false;
        }

    }
}
