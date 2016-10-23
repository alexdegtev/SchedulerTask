using Debugger.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger.Exceptions
{
    [Serializable]
    public class Exception : IException
    {
        public string ErrorStatus { get; set; }
        public string ErrorCode { get; set; }
        public string MessageError { get; set; }

        public ReferenseResourse ReferenceResource { get; set; }
        public Exception() { }

        public Exception(string error_code, string error_status, string error_message, string ReferenceResourceSourse, string ReferenceResourceScheduler)
        {
            this.ErrorCode = error_code;
            this.ErrorStatus = error_status;
            this.MessageError = error_message;
            this.ReferenceResource = new ReferenseResourse(ReferenceResourceSourse, ReferenceResourceScheduler);
        }



    }
}
