using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace base_dotnet.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base()
        {
        }
        public ForbiddenException(string message) : base(message) { }
        public ForbiddenException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}