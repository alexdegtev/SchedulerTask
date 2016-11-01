using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Debugger.Exceptions
{
    [Serializable]
    public class ReferenseResourse
    {
        [XmlElement]
        public string Source { get; set; }
        [XmlElement]
        public string ScheduleFile { get; set; }
        public ReferenseResourse() { }

        public ReferenseResourse(string sourse, string schedule_file)
        {
            this.Source = sourse;
            this.ScheduleFile = schedule_file;
        }
    }
}
