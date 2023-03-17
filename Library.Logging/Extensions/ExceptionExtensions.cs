using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public static  class ExceptionExtensions
    {
        public static string GetAllMessage(this Exception exception)
        {
            var messages = exception.FromHierarchies(ex => ex.InnerException)
                .Select(ex => ex.Message);
            return String.Join(Environment.NewLine, messages);
        }
        public static List<string> GetAllMessageList(this Exception exception)
        {
            var message= exception.FromHierarchies(ex => ex.InnerException).Select(ex => ex.Message).ToList();
            return message;
        }
    }


