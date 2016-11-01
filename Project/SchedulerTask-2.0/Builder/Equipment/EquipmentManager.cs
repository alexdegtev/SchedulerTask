using System;

namespace Builder.Equipment
{
   static class EquipmentManager
    {
        /// <summary>
        /// Поиск свободного оборудования в списке; (возвращаем true, если находим свободное оборудование, false - иначе);
        /// Доп. выходные параметры:
        /// operationtime - время окончания операции (для первого случая) или  ближайшее время начала операции (для второго случая); 
        /// </summary>
        internal static bool IsFree(DateTime T, IOperation o, out DateTime operationTime, out SingleEquipment equip)
        {
            TimeSpan t = o.GetDuration();

            foreach (SingleEquipment e in o.GetEquipment())
            {
                int intervalIndex;
                if ((e.IsNotOccupied(T)) && (e.GetCalendar().IsInterval(T, out intervalIndex)))
                {
                    equip = e;
                    operationTime = e.GetCalendar().GetTimeofRelease(T, t, intervalIndex);
                    return true;
                }
            }

            equip = null;
            DateTime mintime = DateTime.MaxValue;
            foreach (SingleEquipment e in o.GetEquipment())
                if (e.GetCalendar().GetNearestStart(T) <= mintime) mintime = e.GetCalendar().GetNearestStart(T);
            operationTime = mintime;

            return false;
        }
    }
}
