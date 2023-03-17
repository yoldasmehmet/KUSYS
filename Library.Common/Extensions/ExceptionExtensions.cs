using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public static  class ExceptionExtensions
    {
    /// <summary>
    /// Tüm hataları string olarak birleştirip dönderen metod.(Recursive hataları toplar)
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
        public static string GetAllMessages(this Exception exception)
        {
            var messages = exception.FromHierarchy(ex => ex.InnerException)
                .Select(ex => ex.Message);
            return String.Join(Environment.NewLine, messages);
        }
    /// <summary>
    /// Tüm hataları listeleyip dönderen metod.(Recursive hataları toplar)
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static List<string> GetAllMessageList(this Exception exception)
        {
            var message= exception.FromHierarchy(ex => ex.InnerException).Select(ex => ex.Message).ToList();
            return message;
        }
    }


