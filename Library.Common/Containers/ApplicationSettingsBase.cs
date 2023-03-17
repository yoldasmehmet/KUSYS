using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Containers
{
    public class ApplicationSettingsBase
    {

        private static bool isRegistered;
        public  static bool IsRegistered { get => isRegistered; }
        public  static void SetAsRegistered()
        {
            isRegistered = true;
        }
        public static void CheckIsRegistered(string errorMessage)
        {
            if (!IsRegistered)
            {
                throw new Exception(errorMessage);
            }
        }

    }
}
