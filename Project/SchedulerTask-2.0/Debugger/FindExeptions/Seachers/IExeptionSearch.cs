using Debugger.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger.FindExeptions.Seachers
{
    public interface IExeptionSearch
    {
        List<IExeption> Execute();
    }
}
