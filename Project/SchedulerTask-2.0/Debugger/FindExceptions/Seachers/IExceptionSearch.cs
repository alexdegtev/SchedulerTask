using Debugger.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger.FindExceptions.Seachers
{
    public interface IExceptionSearch
    {
        List<IException> Execute();
    }
}
