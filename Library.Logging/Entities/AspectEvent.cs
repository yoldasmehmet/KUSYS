using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logging.Entities
{
    public enum AspectEvent
    {
        [Description("OnException")]
        OnException,
        [Description("OnSucceded")]
        OnSuccess,
        [Description("OnEntry")]
        OnEntry
    }
}
