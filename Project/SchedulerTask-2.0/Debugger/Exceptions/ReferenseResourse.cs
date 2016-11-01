﻿using System;
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
            Source = sourse;
            ScheduleFile = schedule_file;
        }
    }
}
