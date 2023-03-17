using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logging.Entities
{
    public enum MessgageType
    {
        [Description("Error")]
        Error,
        [Description("Information")]
        Information,
        [Description("Warning")]
        Warninig
    }
}
