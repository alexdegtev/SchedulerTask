using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder;

namespace Debugger.IO
{
    public class Reader
    {
        List<Decision> decision;
        /// <summary>
        /// Конструктор ридера
        /// </summary>
        /// <param name="folderPatch" путь к директории построенного расписания></param>
        public Reader(string folderPatch)
        {

        }
        /// <summary>
        /// Считать данные
        /// </summary>
        /// <param name="decision" Список опердаций с построенным расписанием></param>
        public void ReadData(out List<Decision> decision)
        {
            decision = null;

        }
    }
}
