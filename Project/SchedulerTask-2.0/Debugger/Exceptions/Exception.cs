using Debugger.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace Debugger.Exceptions
{
    [Serializable, XmlType("Error")]
    public class Exception : IException
    {
        [XmlElement]
        public string MessageError { get; set; }
        [XmlElement]
        public string ErrorCode { get; set; }
        [XmlElement]
        public string ErrorStatus { get; set; }


        [XmlElement]
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
