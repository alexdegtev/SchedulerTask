using System;
using CommonTypes.Equipment;
using CommonTypes.Operation;

namespace Builder.Equipment
{
   static class EquipmentManager
    {
        /// <summary>
        /// Поиск свободного оборудования в списке
        /// </summary>
        /// <param name="time"></param>
        /// <param name="operation"></param>
        /// <param name="operationTime">время окончания операции (для первого случая) или  ближайшее время начала операции (для второго случая)</param>
        /// <param name="equipment"></param>
        /// <returns>возвращаем true, если находим свободное оборудование, false - иначе</returns>
        internal static bool IsFree(DateTime time, IOperation operation, out DateTime operationTime, out SingleEquipment equipment)
        {
            TimeSpan t = operation.GetDuration();
            int intervalIndex;

            foreach (SingleEquipment e in operation.GetEquipment())
            {
                if ((e.IsNotOccupied(time)) && (e.GetCalendar().IsInterval(time, out intervalIndex)) && (e.GetCalendar().WillRelease(time, t, intervalIndex)))
                {
                    equipment = e;
                    operationTime = e.GetCalendar().GetTimeofRelease(time, t, intervalIndex);
                    return true;
                }
            }

            equipment = null;
            DateTime mintime = DateTime.MaxValue;
            foreach (SingleEquipment e in operation.GetEquipment())
                if (e.GetCalendar().GetNearestStart(time) <= mintime) mintime = e.GetCalendar().GetNearestStart(time);
            operationTime = mintime;

            return false;
        }
    }
}
