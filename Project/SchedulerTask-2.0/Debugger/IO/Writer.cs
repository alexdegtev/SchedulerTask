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
        /// <param name="exeptionList"></param>
        public void WriteLog(List<IException> exeptionList)
        {
            if (File.Exists(folderPath + "exceptions.xml")) File.Delete(folderPath + "exceptions.xml");

            using (var myFile = File.Create(folderPath + "exceptions.xml")) { }
            foreach (IException e in exeptionList)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Debugger.Exceptions.Exception));

                using (TextWriter writer = new StreamWriter(folderPath + "exceptions.xml"))
                {

                    serializer.Serialize(writer, e);
                }
            }
        }
    }
}
