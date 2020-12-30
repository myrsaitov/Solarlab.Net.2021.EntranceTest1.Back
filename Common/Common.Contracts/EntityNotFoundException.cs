using System;
using System.Runtime.Serialization;

namespace BusinessLogic.Contracts.CustomExceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(int id, string entityType)
            : base($"Cущность \"{entityType}\" с идентификатором {id} не найдена.")
        {

        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}