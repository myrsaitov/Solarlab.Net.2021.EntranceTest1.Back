using System;
using System.Runtime.Serialization;

namespace BusinessLogic.Contracts.CustomExceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }
        public BusinessException(string message) : base(message)
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