using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Debugger.IO
{
    class LogReader
    {
        string input;
        public LogReader(string input)
        {
            this.input = input;
        }
        public void ReadData(out Exceptions.ExceptionsList e)
        {
            

            using (XmlReader reader = XmlReader.Create(input + "exceptions.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Exceptions.ExceptionsList));
                e = (Exceptions.ExceptionsList)serializer.Deserialize(reader);
            }
        }
    }
}
