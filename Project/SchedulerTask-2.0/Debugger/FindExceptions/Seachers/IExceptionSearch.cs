using Debugger.Exceptions;
using System.Collections.Generic;

namespace Debugger.FindExceptions.Seachers
{
    public interface IExceptionSearch
    {
        List<IException> Execute();
    }
}
