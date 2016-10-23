using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger.Exceptions
{
    [Serializable]
    public class ReferenseResourse
    {
        public string Source { get; set; }
        public string ScheduleFile { get; set; }
        public ReferenseResourse() { }

        public ReferenseResourse(string sourse, string schedule_file)
        {
            this.Source = sourse;
            this.ScheduleFile = schedule_file;
        }
    }
}
