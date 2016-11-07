using Debugger.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Debugger.IO
{
    public class Writer
    {
        string folderPath;
        public Writer(string folderPath)
        {
            this.folderPath = folderPath;
        }
        /// <summary>
        /// Записать результат в .xml // XmlSerializer
        /// </summary>
        /// <param name="exeptions"></param>
        public void WriteLog(List<IException> exeptions)
        {
            if (File.Exists(folderPath + "exceptions.xml")) File.Delete(folderPath + "exceptions.xml");

            ExceptionsList exceptionsList = new ExceptionsList(exeptions);

            using (TextWriter writer = new StreamWriter(folderPath + "exceptions.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ExceptionsList));
                serializer.Serialize(writer, exceptionsList);
            }
        }
    }
}
