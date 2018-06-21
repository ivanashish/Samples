using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace BankApplication.CustomExceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string format, params object[] args) : base(string.Format(CultureInfo.InvariantCulture, format, args))
        {
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
