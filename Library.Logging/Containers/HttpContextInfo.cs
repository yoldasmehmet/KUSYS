using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Logging.Containers
{
    public class HttpContextInfo
    {
        public string UserInfo { get; set; }
        public string Host { get; set; }
        public string ClientIp { get; set; }
        public string RequestPath { get; set; }
        public string SessionInfo { get; set; }
        public int StatusCode { get; set; }
    }
}
