using Debagger.Exeptions;
using Debugger.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debagger.Exeption
{
    [Serializable]
    public class Exeption : IExeption
    {
        public string ErrorStatus { get; set; }
        public string ErrorCode { get; set; }
        public string MessageError { get; set; }

        public ReferenseResourse ReferenceResource { get; set; }
        public Exeption() { }

        public Exeption(string error_code, string error_status, string error_message, string ReferenceResourceSourse, string ReferenceResourceScheduler)
        {
            this.ErrorCode = error_code;
            this.ErrorStatus = error_status;
            this.MessageError = error_message;
            this.ReferenceResource = new ReferenseResourse(ReferenceResourceSourse, ReferenceResourceScheduler);
        }



    }
}
