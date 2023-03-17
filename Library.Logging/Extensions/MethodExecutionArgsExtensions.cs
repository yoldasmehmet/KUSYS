using Libary.Logging.Extensions;
using MethodBoundaryAspect.Fody.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.Logging.Extensions
{
    public static class MethodExecutionArgsExtensions
    {
        public static string GetMethodExecutionUniqueId(this MethodExecutionArgs args)
        {
            var typeofInstance = args.Instance.GetType();
            var id = $"{typeofInstance.FullName}_{args.Method.Name}_{args.Arguments.ToJson()}";
            args.MethodExecutionTag = id;
            return id;
        }
    }
}