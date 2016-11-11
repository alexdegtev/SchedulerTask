using System;
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

        public Exception(string errorCode, string errorStatus, string errorMessage, string referenceResourceSourse, string referenceResourceScheduler)
        {
            this.ErrorCode = errorCode;
            this.ErrorStatus = errorStatus;
            this.MessageError = errorMessage;
            this.ReferenceResource = new ReferenseResourse(referenceResourceSourse, referenceResourceScheduler);
        }
    }
}
