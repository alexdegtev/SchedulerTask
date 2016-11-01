using System;
using System.Xml.Serialization;

namespace Debugger.Exceptions
{
    [Serializable, XmlType("Error")]
    public class Exception : IException
    {
        [XmlElement]
        public string ErrorCode { get; set; }

        [XmlElement]
        public string ErrorStatus { get; set; }

        [XmlElement]
        public string MessageError { get; set; }

        [XmlElement]
        public ReferenseResourse ReferenceResource { get; set; }

        public Exception() { }

        public Exception(string error_code, string error_status, string error_message, string ReferenceResourceSourse, string ReferenceResourceScheduler)
        {
            ErrorCode = error_code;
            ErrorStatus = error_status;
            MessageError = error_message;
            ReferenceResource = new ReferenseResourse(ReferenceResourceSourse, ReferenceResourceScheduler);
        }
    }
}
