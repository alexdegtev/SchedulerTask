using Debugger.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.FindExceptions.Seachers
{
    public interface IExceptionSearch
    {
        List<IException> Execute();
    }
}
