using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Security
{
    public class SecurityConfig
    {
        public string? AccessKey { get; set; }
        public string? SecretKey { get; set; }
        public string? Host { get; set; }
        public string? Url { get; set; }
        public string? BucketName { get; set; }

    }
}
