using Libary.Logging.Containers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logging.Entities
{
    public class LogContainer
    {
        //public SessionInfo Session { get; set; }

        public string AssamblyName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string ParameterValues { get; set; }
        public DateTime LogTime { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string AspectEvent { get; set; }
        public int MessageNo { get; set; }
        public string ReturnValueJson { get; set; }
        public HttpContextInfo ContextInfo { get; set; }
    }
}
