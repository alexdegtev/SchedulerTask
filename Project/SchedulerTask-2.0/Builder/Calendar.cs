using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    /// <summary>
    /// Class for calendar.
    /// </summary>
    public class Calendar
    {
        public struct Interval
        {
            public DateTime startTime;
            public DateTime endTime;
        }

        private DateTime m_startDate;
        private DateTime m_endDate;
        private List<Interval> m_workTime;

        public Calendar(DateTime startDate, DateTime endDate) { m_startDate = startDate; m_endDate = endDate; m_workTime = new List<Interval>(); }

        // Метод возвращает истину, если переданная дата date в календаре свободна
        public bool IsFreeTime(DateTime date)
        {
            foreach (Interval item in m_workTime)
            {
                if (item.startTime >= date && item.endTime < date)
                    return false;
            }
            return true;
        }

        // Метод возвращает истину, если переданная дата date попадает на занятое время в календаре
        public bool IsWorkTime(DateTime date)
        {
            return !IsFreeTime(date);
        }

        // Метод помечает время в календаре как занятое. Возвращает истину, если это сделать удалось.
        //  - startTime     - дата и время, с которых нужно занять оборудование. Если это время занято, то
        //                  - то метод вернёт значение false
        // - workDuration   - интервал времени, который нужно занять, без учёта перерывов в работе
        // - workInterval   - выходная переменная, в которую будет записано время начала и оканчания операции
        public bool SetWorkTime(DateTime startTime, TimeSpan workDuration, out Interval workInterval)
        {
            workInterval = new Interval();
            return false;
        }

        // Метод находит близжайшую свободную дату к дате startDate, и записывает найденную дату в spareDate
        // Возвращает истину, если такую дату удалось найти в календаре.
        public bool GetNearestSpareTime(DateTime startDate, out DateTime spareDate)
        {
            spareDate = new DateTime();
            return false;
        }
    }
}
